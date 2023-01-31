using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;

namespace ApplicationCore.Interfaces.ContextInterfaces.RouteTripBuilder
{
    public interface IDirectorRouteTripBuilder
    {
        public IQueryable<RouteTrip> IncludeRouteBuilder();
        public IQueryable<RouteTrip> IncludeRoutesBuilder();
        public IQueryable<RouteTrip> IncludeDriverBuilder();
        public IQueryable<RouteTrip> IncludeAllBuilder();

    }
}