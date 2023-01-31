using System;
using System.Collections.Generic;
using ApplicationCore.Entities.Values.Enums;

namespace ApplicationCore.Entities.AppEntities.Orders
{
    public class Delivery : BaseEntity
    {
        public Delivery ()
        {
            CreatedAt = DateTime.Now;
        }
        
        public State State { get; set; }
        public RouteTrip RouteTrip { get; set; }

        public DateTime CreatedAt { get; private set;}
        public DateTime? DeliveryDate { get; set; }
        public DateTime? CompletionDate { get; private set; }
        public DateTime? CancellationDate { get; private set; }
        public bool IsDeleted { get; set; }
        public List<Order> Orders { get; private set; } = new();


        public Delivery AddOrder(Order order)
        {
            Orders?.Add(order);
            return this;
        }
        public void UpdateCompletionDate(DateTime dateTime)
        {
            CompletionDate = dateTime;
        }
        public void UpdateCancellationDate(DateTime dateTime)
        {
            CancellationDate = dateTime;
        }
    }
}