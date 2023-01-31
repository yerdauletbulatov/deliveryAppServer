using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.Values.Enums;
using BackgroundTasks.Interfaces;
using BackgroundTasks.Model;
using Infrastructure.AppData.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundTasks
{
    public class BackgroundTask : BackgroundService
    {    
        private readonly ILogger<BackgroundTask> _logger;
        private readonly IBackgroundTaskQueue _backgroundQueue;
        private readonly IServiceScopeFactory _serviceProvider;
        public BackgroundTask(IBackgroundTaskQueue backgroundQueue, IServiceScopeFactory serviceProvider, ILogger<BackgroundTask> logger)
        {
            _backgroundQueue = backgroundQueue;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BackgroundTask service running.");
            while (!stoppingToken.IsCancellationRequested)
            {
                var backgroundOrder = await _backgroundQueue.DequeueAsync(stoppingToken);
                if (backgroundOrder is null) continue;
                while (backgroundOrder.WaitingPeriodTime > DateTime.Now)
                {
                    _logger.LogInformation("Waiting order state. {0} : {1}", backgroundOrder.WaitingPeriodTime.ToString("T"), DateTime.Now.ToString("T"));
                    await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
                }
                await DoWork(backgroundOrder, stoppingToken);
            }
        }

        private async ValueTask DoWork(BackgroundOrder backgroundOrder, CancellationToken stoppingToken)
        {
            var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
            var order = await OrderAsync(dbContext, backgroundOrder);
            if (CheckState(order, backgroundOrder.DeliveryId))
            {
                await RejectedOrderAsync(order, dbContext, stoppingToken);
                _logger.LogInformation("Complete work!");
            }
        }
        
        private async Task<Order> OrderAsync(AppDbContext dbContext,BackgroundOrder backgroundOrder) =>
            await dbContext.Orders
                .Include(o => o.State)
                .Include(o => o.Delivery.RouteTrip)
                .FirstOrDefaultAsync(o =>
                    o.Id == backgroundOrder.OrderId && 
                    o.Delivery.Id == backgroundOrder.DeliveryId);  //TODO builder

        private bool CheckState(Order order, int deliveryId) =>
            order?.State.Id == (int)GeneralState.OnReview && 
            order.Delivery.Id == deliveryId;

        private async Task RejectedOrderAsync(Order order,AppDbContext dbContext, CancellationToken stoppingToken)
        {
            var rejectedOrder = new RejectedOrder
            {
                Order = order,
                RouteTrip = order.Delivery.RouteTrip
            };
            order.State = await dbContext.States.FirstOrDefaultAsync(s => s.Id == (int)GeneralState.Waiting, stoppingToken);
            order.Delivery = null;
            await dbContext.RejectedOrders.AddAsync(rejectedOrder, stoppingToken);
            await dbContext.SaveChangesAsync(stoppingToken);
        }
    }
}