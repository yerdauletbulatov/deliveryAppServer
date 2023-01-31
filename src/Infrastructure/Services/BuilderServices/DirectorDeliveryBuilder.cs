using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Interfaces.ContextInterfaces.DeliveryBuilder;

namespace Infrastructure.Services.BuilderServices
{
    public class DirectorDeliveryBuilder : IDirectorDeliveryBuilder
    {
        private readonly IDeliveryBuilder _builder;

        public DirectorDeliveryBuilder(IDeliveryBuilder builder)
        {
            _builder = builder;
        }
        
        public IQueryable<Delivery> IncludeRouteBuilder() =>
            _builder.IncludeRoute()
                .Build();     
        
        public IQueryable<Delivery> IncludeRouteTripAndRouteBuilder() =>
            _builder.IncludeRouteTrip().IncludeRoute()
                .Build();     
        public IQueryable<Delivery> IncludeRouteTripAndDriverBuilder() =>
            _builder.IncludeRouteTrip().IncludeDriver()
                .Build();
    }
}