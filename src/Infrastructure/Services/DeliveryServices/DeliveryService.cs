using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.AppEntities.Locations;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.Values;
using ApplicationCore.Entities.Values.Enums;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.DeliveryInterfaces;
using Infrastructure.AppData.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.DeliveryServices
{
    public class DeliveryService : IDelivery
    {
        private readonly AppIdentityDbContext _identityDbContext;
        private readonly IContext _context;

        public DeliveryService(AppIdentityDbContext identityDbContext, IContext context)
        {
            _identityDbContext = identityDbContext;
            _context = context;
        }

        public async Task<Order> AddToDeliveryAsync(int orderId)
        {
            var order = await _context.Orders().IncludeClientBuilder().FirstAsync(c => c.Id == orderId);
            order.State = await _context.FindAsync<State>((int)GeneralState.PendingForHandOver);
            await _context.UpdateAsync(order);
            return order;
        }


        public async Task<ActionResult> GetActiveOrdersForClientAsync(string userClientId)
        {
            var deliveriesInfo = new List<DeliveryInfo>();
            var userClient = await _identityDbContext.Users.FirstAsync(u => u.Id == userClientId);

            var orders = await OrdersAsync(userClientId);
            foreach (var order in orders)
            {
                var userDriver = await _identityDbContext.Users.FirstAsync(u => u.Id == order.Delivery.RouteTrip.Driver.UserId);
                var deliveryInfo = order.GetDeliveryInfo(userClient, userDriver);
                var driverLocation = await _context.GetAll<LocationDate>()
                    .Include(r => r.Location)
                    .FirstOrDefaultAsync(l => l.RouteTrip.Id == order.Delivery.RouteTrip.Id);  //TODO builder
                deliveryInfo.Location = driverLocation?.Location;
                deliveriesInfo.Add(deliveryInfo);
            }
            return new OkObjectResult(deliveriesInfo);
        }
        
        public async Task<Delivery> FindIsActiveDelivery(Order order, CancellationToken cancellationToken)
        {
            var deliveries = await Deliveries(order, cancellationToken);
            foreach (var delivery in deliveries)
            {
                if (await CheckRejectedAsync(delivery.RouteTrip.Id, order.Id)) continue;
                return delivery;
            }
            return null;
        }
        
        private async Task<List<Delivery>> Deliveries(Order order, CancellationToken cancellationToken) => 
            await _context
                .Deliveries()
                .IncludeRouteTripAndDriverBuilder()
                .Where(r => 
                    r.RouteTrip.Route.StartCity.Id == order.Route.StartCityId && 
                    r.RouteTrip.Route.FinishCity.Id == order.Route.FinishCityId && 
                    r.RouteTrip.DeliveryDate >= order.DeliveryDate)
                .ToListAsync(cancellationToken);
        
        private async Task<bool> CheckRejectedAsync(int routeTripId, int orderId) => 
            await _context
                .AnyAsync<RejectedOrder>(r =>
                    r.RouteTrip.Id == routeTripId &&
                    r.Order.Id == orderId);

        private async Task<List<Order>> OrdersAsync(string userClientId)
        {
            var stateInProgress =  await _context.FindAsync<State>((int)GeneralState.InProgress);
            var stateHandOver = await _context.FindAsync<State>((int)GeneralState.PendingForHandOver);
            var stateReceived = await _context.FindAsync<State>((int)GeneralState.ReceivedByDriver);
            return await _context 
                .Orders()
                .IncludeDeliveriesInfoBuilder()
                .Where(c =>
                    c.Client.UserId == userClientId &&
                    c.Delivery.RouteTrip.IsActive &&
                    (c.State == stateInProgress || c.State == stateHandOver || c.State == stateReceived)).ToListAsync();
        }

    }
}