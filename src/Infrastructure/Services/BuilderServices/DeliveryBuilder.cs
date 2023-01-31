using System.Linq;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Interfaces.ContextInterfaces.DeliveryBuilder;
using Infrastructure.AppData.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.BuilderServices
{
    public class DeliveryBuilder : IDeliveryBuilder
    {
        private IQueryable<Delivery> _deliveries;
        private readonly AppDbContext _db;

        public DeliveryBuilder(AppDbContext dbContext)
        {
            _db = dbContext;
            _deliveries = _db.Deliveries;
        }
        
        public IDeliveryBuilder IncludeState()
        {
            _deliveries = _deliveries.Include(d => d.State);
            return this;
        }

        public IDeliveryBuilder IncludeRouteTrip()
        {
            _deliveries = _deliveries.Include(d => d.RouteTrip);
            return this;
        }

        public IDeliveryBuilder IncludeRoute()
        {
            _deliveries = _deliveries.Include(d => d.RouteTrip.Route);
            return this;
        }

        public IDeliveryBuilder IncludeRoutes()
        {
            _deliveries = _deliveries.Include(d => d.RouteTrip.Route.StartCity)
                .Include(d => d.RouteTrip.Route.FinishCity);
            return this;
        }

        public IDeliveryBuilder IncludeDriver()
        {
            _deliveries = _deliveries.Include(d => d.RouteTrip.Driver);
            return this;
        }

        public IQueryable<Delivery> Build()
        {
            var deliveries = _deliveries;
            _deliveries = _db.Deliveries;
            return deliveries;
        }
    }
}