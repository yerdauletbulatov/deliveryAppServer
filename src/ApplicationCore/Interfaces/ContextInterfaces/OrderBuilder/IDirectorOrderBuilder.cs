using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;

namespace ApplicationCore.Interfaces.ContextInterfaces.OrderBuilder
{
    public interface IDirectorOrderBuilder
    {
        public IQueryable<Order> IncludeRouteBuilder();
        public IQueryable<Order> IncludeClientBuilder();

        public IQueryable<Order> IncludeOrdersInfoBuilder();

        public IQueryable<Order> IncludeRouteTripBuilder();

        public IQueryable<Order> IncludeDeliveriesInfoBuilder();
        public IQueryable<Order> IncludeForRejectBuilder();
    }
}