using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.AppEntities.Cars;
using ApplicationCore.Entities.AppEntities.Locations;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.AppEntities.Routes;
using ApplicationCore.Entities.Values;
using ApplicationCore.Entities.Values.Enums;
using ApplicationCore.Interfaces.ClientInterfaces;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.DriverInterfaces;
using Infrastructure.AppData.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ClientServices
{
    public class OrderService : IOrder
    {
        private readonly AppIdentityDbContext _dbIdentityDbContext;
        private readonly IContext _context;

        public OrderService(AppIdentityDbContext dbIdentityDbContext, IContext context)
        {
            _dbIdentityDbContext = dbIdentityDbContext;
            _context = context;
        }

        public async Task<Order> CreateAsync(OrderInfo info, string clientUserId, CancellationToken cancellationToken)
        {
            var client = await _context.FindAsync<Client>(c => c.UserId == clientUserId);
            var carType = await _context.FindAsync<CarType>(c => c.Id == info.CarType.Id);
            var route = await _context.FindAsync<Route>(r =>
                r.StartCityId == info.StartCity.Id &&
                r.FinishCityId == info.FinishCity.Id);
            var state = await _context.FindAsync<State>((int)GeneralState.Waiting);
            var order = new Order(info.IsSingle, info.Price, info.DeliveryDate)
            {
                Client = client,
                Package = info.Package,
                CarType = carType,
                Route = route,
                State = state,
                Location = new Location(info.Location.Latitude, info.Location.Longitude)
            };
            await _context.AddAsync(order);
            return order;
        }

        public async Task UpdateOrderAsync(Order order, Delivery delivery, int stateId)
        {
            order.State = await _context.FindAsync<State>(stateId);
            order.Delivery = delivery;
            await _context.UpdateAsync(order);
        }

        public async Task<bool> AnyWaitingOrdersAsync(Delivery delivery)
        {
            var stateWaiting = await _context.FindAsync<State>((int)GeneralState.Waiting);
            var stateOnReview = await _context.FindAsync<State>((int)GeneralState.OnReview);;
            var waitingOrders = await _context.Orders().IncludeOrdersInfoBuilder().Where(o =>
                o.Route.Id == delivery.RouteTrip.Route.Id &&
                o.DeliveryDate.Day <= delivery.RouteTrip.DeliveryDate.Day &&
                o.State == stateWaiting).ToListAsync();
            foreach (var waitingOrder in waitingOrders)
            {
                waitingOrder.State = stateOnReview;
                delivery.AddOrder(waitingOrder);
            }
            await _context.UpdateAsync(delivery);
            return waitingOrders.Any();
        }


        public async Task<ActionResult> GetWaitingOrdersAsync(string clientUserId, CancellationToken cancellationToken)
        {
            return new OkObjectResult(await GetOrderInfoAsync(clientUserId, GeneralState.Waiting));
        }

        public async Task<ActionResult> GetOnReviewOrdersAsync(string clientUserId, CancellationToken cancellationToken)
        {
            return new OkObjectResult(await GetOrderInfoAsync(clientUserId, GeneralState.OnReview));
        }

        private async Task<List<OrderInfo>> GetOrderInfoAsync(string clientUserId, GeneralState status)
        {
            var ordersInfo = new List<OrderInfo>();
            var userClient = await _dbIdentityDbContext.Users.FirstOrDefaultAsync(u => u.Id == clientUserId);
            var state = await _context.FindAsync<State>((int)status);
            await _context.Orders()
                .IncludeOrdersInfoBuilder()
                .Where(o => o.Client.UserId == clientUserId && o.State == state)
                .ForEachAsync(o => ordersInfo.Add(o.GetOrderInfo(userClient)));
            return ordersInfo;
        }
    }
}