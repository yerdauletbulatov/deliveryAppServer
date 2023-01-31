using System;
using ApplicationCore.Entities.AppEntities.Routes;

namespace ApplicationCore.Entities.AppEntities.Orders
{
    public class RouteTrip : BaseEntity
    {
        public RouteTrip(DateTime deliveryDate)
        {
            DeliveryDate = deliveryDate;
            CreatedAt = DateTime.Now;
            IsActive = true;
        }

        public Driver Driver { get; set;}
        public Route Route { get; set;}
        public DateTime CreatedAt { get; private set; }
        public DateTime DeliveryDate { get; private set; }
        public bool IsActive { get; private set; }
        
    }
}