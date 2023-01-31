using System;
using ApplicationCore.Entities.AppEntities.Locations;
using ApplicationCore.Entities.AppEntities.Routes;
using MediatR;

namespace PublicApi.Commands
{
    public class CreateDeliveryCommand : IRequest
    {
        public City StartCity { get; set; }
        public City FinishCity { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Location Location { get; set; }
        public string UserId { get; private set; }
        public CreateDeliveryCommand SetUserId(string userId)
        {
            UserId = userId;
            return this;
        }
    }
}