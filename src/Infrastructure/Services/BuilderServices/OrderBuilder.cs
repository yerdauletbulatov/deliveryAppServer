using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Interfaces.ContextInterfaces.OrderBuilder;
using Infrastructure.AppData.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.BuilderServices
{
    public class OrderBuilder : IOrderBuilder
    {
        private IQueryable<Order> _orders;
        private readonly AppDbContext _db;
        public OrderBuilder(AppDbContext db)
        {
            _db = db;
            _orders = _db.Orders;
        }
        public IOrderBuilder IncludeRoutes()
        {
            _orders = _orders.Include(o => o.Route.StartCity)
                .Include(o => o.Route.FinishCity);
            return this;
        }      
            public IOrderBuilder IncludeRoute()
        {
            _orders = _orders.Include(o => o.Route);
            return this;
        }   
        public IOrderBuilder IncludeState()
        {
            _orders = _orders.Include(o => o.State);
            return this;
        }

        public IOrderBuilder IncludeOrdersInfo()
        {
            _orders = _orders 
                .Include(o => o.Package)
                .Include(o => o.CarType)
                .Include(o => o.Location);
            return this;
        }

        public IOrderBuilder IncludeClient()
        {
            _orders = _orders.Include(o => o.Client);
            return this;
        }
        public IOrderBuilder IncludeRouteTrip()
        {
            _orders = _orders.Include(o => o.Delivery.RouteTrip);
            return this;
        }    
        public IOrderBuilder IncludeDeliveryOrders()
        {
            _orders = _orders.Include(o => o.Delivery.RouteTrip);
            return this;
        }

        public IOrderBuilder IncludeDeliveriesInfo()
        {
            _orders = _orders.Include(o => o.Delivery)
                .Include(o => o.Delivery.RouteTrip.Driver);
            return this;
        }

        public IQueryable<Order> Build()
        {
            var orders = _orders;
            _orders = _db.Orders;
            return orders;
        }
    }
}