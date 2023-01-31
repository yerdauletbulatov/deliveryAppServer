using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;

namespace ApplicationCore.Interfaces.ContextInterfaces.OrderBuilder
{
    public interface IOrderBuilder
    {
        public IOrderBuilder IncludeRoutes();
        public IOrderBuilder IncludeOrdersInfo();
        public IOrderBuilder IncludeClient();
        public IOrderBuilder IncludeRouteTrip();
        public IOrderBuilder IncludeDeliveriesInfo();
        public IOrderBuilder IncludeDeliveryOrders();
        public IOrderBuilder IncludeRoute();
        public IOrderBuilder IncludeState();
        public IQueryable<Order> Build();
    }
}