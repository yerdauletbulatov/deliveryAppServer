using System;
using ApplicationCore.Entities.AppEntities.Cars;
using ApplicationCore.Entities.AppEntities.Locations;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.AppEntities.Routes;
using MediatR;

namespace PublicApi.Commands
{
    public class CreateOrderCommand : IRequest
    {
        public City StartCity { get; set; }
        public City FinishCity { get; set; }
        public Package Package { get; set; }
        public DateTime DeliveryDate { get; set; }
        public CarType CarType { get; set; }
        public bool IsSingle { get; set; }
        public double Price { get; set; }
        public Location Location { get; set; }
        public string UserId { get; private set; }
        public CreateOrderCommand SetUserId(string userId)
        {
            UserId = userId;
            return this;
        }
    }
}