using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Interfaces.ContextInterfaces.RouteTripBuilder;

namespace Infrastructure.Services.BuilderServices
{
    public class DirectorRouteTripBuilder : IDirectorRouteTripBuilder
    {
        private readonly IRouteTripBuilder _builder;

        public DirectorRouteTripBuilder(IRouteTripBuilder builder)
        {
            _builder = builder;
        }

        public IQueryable<RouteTrip> IncludeRouteBuilder() =>
            _builder.IncludeRoute().Build();

        public IQueryable<RouteTrip> IncludeRoutesBuilder() =>
            _builder.IncludeRoutes().Build();

        public IQueryable<RouteTrip> IncludeDriverBuilder() =>
            _builder.IncludeDriver().Build();

        public IQueryable<RouteTrip> IncludeAllBuilder() =>
            _builder.IncludeRoute().IncludeDriver().Build();
    }
}