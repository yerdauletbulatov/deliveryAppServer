using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Interfaces.ContextInterfaces.RouteTripBuilder;
using Infrastructure.AppData.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.BuilderServices
{
    public class RouteTripBuilder : IRouteTripBuilder
    {
        private IQueryable<RouteTrip> _routeTrips;
        private readonly AppDbContext _db;

        public RouteTripBuilder(AppDbContext db)
        {
            _db = db;
            _routeTrips = _db.RouteTrips;
        }

        public IRouteTripBuilder IncludeRoutes()
        {
            _routeTrips = _routeTrips.Include(r => r.Route.StartCity).Include(r => r.Route.FinishCity);
            return this;
        }

        public IRouteTripBuilder IncludeRoute()
        {
            _routeTrips = _routeTrips.Include(r => r.Route);
            return this;
        }

        public IRouteTripBuilder IncludeDriver()
        {
            _routeTrips = _routeTrips.Include(r => r.Driver);
            return this;
        }

        public IQueryable<RouteTrip> Build()
        {
            var trips = _routeTrips;
            _routeTrips = _db.RouteTrips;
            return trips;
        }
    }
}