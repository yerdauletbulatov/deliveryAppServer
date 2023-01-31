using System;
using ApplicationCore.Entities.AppEntities.Cars;
using ApplicationCore.Entities.AppEntities.Locations;
using ApplicationCore.Entities.AppEntities.Routes;
using ApplicationCore.Entities.Values.Enums;

namespace ApplicationCore.Entities.AppEntities.Orders
{
    public class Order : BaseEntity
    {
        public Order(bool isSingle, decimal price, DateTime deliveryDate)
        {
            DeliveryDate = deliveryDate;
            IsSingle = isSingle;
            CreatedAt = DateTime.Now;
            Price = price;
        }
        public CarType CarType { get;  set;}
        public Client Client { get;  set;}
        public Package Package { get;  set;}
        public bool IsSingle { get; private set;}
        public DateTime CreatedAt { get; private set; }
        public DateTime DeliveryDate { get; private set; }
        public State State { get; set; }
        public decimal Price { get; private set;}
        public Location Location { get;  set;}
        public Route Route { get; set;}
        public Delivery Delivery { get;  set;}

    }
}