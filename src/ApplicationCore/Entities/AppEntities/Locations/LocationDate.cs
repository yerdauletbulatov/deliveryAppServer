using System;
using ApplicationCore.Entities.AppEntities.Orders;

namespace ApplicationCore.Entities.AppEntities.Locations
{
    public class LocationDate : BaseEntity
    {
        public LocationDate()
        {
            LocationDateTime = DateTime.Now;
        }

        public Location Location { get; set;}
        public RouteTrip RouteTrip { get; set;}
        public DateTime LocationDateTime { get; private set;}
        
    }
}