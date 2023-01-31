using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.Values;
using ApplicationCore.Entities.Values.Enums;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.DriverInterfaces;
using Infrastructure.AppData.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.DriverServices
{
    public class DriverService : IDriver
    {
        private readonly AppIdentityDbContext _identityDbContext;
        private readonly IContext _context;


        public DriverService(AppIdentityDbContext identityDbContext, IContext context)
        {
            _identityDbContext = identityDbContext;
            _context = context;
        }
        
        public async Task<ActionResult> GetOnReviewOrdersForDriverAsync(string driverUserId)
        {
            try
            {
                var delivery = await _context.FindAsync<Delivery>(d => d.RouteTrip.Driver.UserId == driverUserId)
                               ?? throw new NullReferenceException("Для проверки заказов создайте поездку");
                var state = await _context.FindAsync<State>((int)GeneralState.OnReview);
                var ordersInfo = new List<OrderInfo>();
                await _context.Orders()
                    .IncludeOrdersInfoBuilder()
                    .Where(o => o.Delivery.Id == delivery.Id && o.State.Id == state.Id)
                    .ForEachAsync(o =>
                    {
                        var userClient = _identityDbContext.Users.FirstOrDefault(u => u.Id == o.Client.UserId);
                        ordersInfo.Add(o.GetOrderInfo(userClient));
                    });
                return new OkObjectResult(ordersInfo);
            }
            catch (NullReferenceException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<ActionResult> GetActiveOrdersForDriverAsync(string driverUserId)
        {
            try
            {
                var delivery = await _context.FindAsync<Delivery>(d => d.RouteTrip.Driver.UserId == driverUserId)
                               ?? throw new NullReferenceException("Для проверки заказов создайте поездку");
                var stateInProgress = await _context.FindAsync<State>((int)GeneralState.InProgress);
                var stateHandOver = await _context.FindAsync<State>((int)GeneralState.PendingForHandOver);
                var stateReceived = await _context.FindAsync<State>((int)GeneralState.ReceivedByDriver);
                var ordersInfo = new List<OrderInfo>();
                await _context.Orders()
                    .IncludeOrdersInfoBuilder()
                    .Where(o =>
                        o.Delivery.Id == delivery.Id &&
                        (o.State.Id == stateInProgress.Id || o.State.Id == stateHandOver.Id ||
                         o.State.Id == stateReceived.Id))
                    .ForEachAsync(o =>
                    {
                        var userClient = _identityDbContext.Users.FirstOrDefault(u => u.Id == o.Client.UserId);
                        ordersInfo.Add(o.GetOrderInfo(userClient));
                    });
                return new OkObjectResult(ordersInfo);
            }
            catch (NullReferenceException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        public async Task<Order> RejectOrderAsync(int orderId)
        {
            var order = await _context.Orders().IncludeForRejectBuilder()
                .FirstOrDefaultAsync(o => o.Id == orderId);
            await _context.AddAsync(new RejectedOrder
            {
                Order = order,
                RouteTrip = order.Delivery.RouteTrip
            });
            order.Delivery?.Orders.Remove(order);
            order.State = await _context.FindAsync<State>((int)GeneralState.Waiting);
            await _context.UpdateAsync(order);
            return order;
        }
    }
}