using System;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.AppEntities.Cars;
using ApplicationCore.Entities.AppEntities.Locations;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.AppEntities.Routes;

namespace ApplicationCore.Entities.Values
{
    public class OrderInfo
    {
        public int OrderId { get; set; }
        public City StartCity { get; set; }
        public City FinishCity { get; set; }
        public Package Package { get; set; }
        public CarType CarType { get; set; }
        public bool IsSingle { get; set; }
        public decimal Price { get; set; }
        public string StateName { get; set; }
        public string ClientPhoneNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string ClientName { get; set; }
        public string ClientSurname { get; set; } 
        public Location Location  { get; set; }
    }
}