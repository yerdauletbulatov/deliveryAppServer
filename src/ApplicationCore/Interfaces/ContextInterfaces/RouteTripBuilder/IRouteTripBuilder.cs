using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;

namespace ApplicationCore.Interfaces.ContextInterfaces.RouteTripBuilder
{
    public interface IRouteTripBuilder
    {
        public IRouteTripBuilder IncludeRoute();
        public IRouteTripBuilder IncludeRoutes();
        public IRouteTripBuilder IncludeDriver();
        public IQueryable<RouteTrip> Build();
    }
}