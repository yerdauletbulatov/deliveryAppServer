using System;

namespace ApplicationCore.Entities.AppEntities.Orders
{
    public class DeliveryStateHistory : BaseEntity
    {
        public Delivery Delivery { get; set; }
        public State State { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}