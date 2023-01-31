using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities.Orders;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces.DeliveryInterfaces
{
    public interface IDelivery
    {
        public  Task<Delivery> FindIsActiveDelivery(Order order, CancellationToken cancellationToken);
        public Task<ActionResult> GetActiveOrdersForClientAsync(string userClientId);
        public Task<Order> AddToDeliveryAsync(int orderId);

    }
}