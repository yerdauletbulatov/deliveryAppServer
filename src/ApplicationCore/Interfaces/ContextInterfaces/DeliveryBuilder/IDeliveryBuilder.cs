using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;

namespace ApplicationCore.Interfaces.ContextInterfaces.DeliveryBuilder
{
    public interface IDeliveryBuilder
    {
        public IDeliveryBuilder IncludeState();
        public IDeliveryBuilder IncludeRouteTrip();
        public IDeliveryBuilder IncludeRoute();
        public IDeliveryBuilder IncludeRoutes();
        public IDeliveryBuilder IncludeDriver();
        public IQueryable<Delivery> Build();
    }
}