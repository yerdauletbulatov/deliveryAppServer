using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;

namespace ApplicationCore.Interfaces.ContextInterfaces.DeliveryBuilder
{
    public interface IDirectorDeliveryBuilder
    {
        public IQueryable<Delivery> IncludeRouteBuilder();
        public IQueryable<Delivery> IncludeRouteTripAndRouteBuilder();
        public IQueryable<Delivery> IncludeRouteTripAndDriverBuilder();
    }
}