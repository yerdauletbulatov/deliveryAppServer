using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Interfaces.ContextInterfaces.OrderBuilder;

namespace Infrastructure.Services.BuilderServices
{
    public class DirectorOrderBuilder : IDirectorOrderBuilder
    {
        private readonly IOrderBuilder _builder;

        public DirectorOrderBuilder(IOrderBuilder builder)
        {
            _builder = builder;
        }


        public IQueryable<Order> IncludeRouteBuilder() =>
            _builder.IncludeRoutes().Build();

        public IQueryable<Order> IncludeClientBuilder() => 
            _builder.IncludeClient().Build();
        public IQueryable<Order> IncludeOrdersInfoBuilder() =>
            _builder.IncludeRoutes()
                .IncludeClient()
                .IncludeOrdersInfo()
                .IncludeState()
                .Build();

        public IQueryable<Order> IncludeRouteTripBuilder() =>
            _builder.IncludeRoutes()
                .IncludeClient()
                .IncludeState()
                .IncludeOrdersInfo()
                .IncludeRouteTrip()
                .Build();

        public IQueryable<Order> IncludeDeliveriesInfoBuilder() => 
            _builder.IncludeRoutes()
            .IncludeClient()
            .IncludeOrdersInfo()
            .IncludeState()
            .IncludeRouteTrip()
            .IncludeDeliveriesInfo()
            .Build();

        public IQueryable<Order> IncludeForRejectBuilder() =>
            _builder.IncludeRoute()
                .IncludeRouteTrip()
                .IncludeState()
                .IncludeDeliveryOrders()
                .Build();
    }
}