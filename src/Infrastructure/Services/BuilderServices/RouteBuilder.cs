using System.Linq;
using ApplicationCore.Entities.AppEntities.Routes;
using ApplicationCore.Interfaces.ContextInterfaces.RouteBuilder;
using Infrastructure.AppData.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.BuilderServices
{
    public class RouteBuilder : IRouteBuilder
    {
        private IQueryable<Route> _routes;
        private readonly AppDbContext _db;

        public RouteBuilder(AppDbContext dbContext)
        {
            _db = dbContext;
            _routes = _db.Routes;
        }

        public IRouteBuilder IncludeRoutes()
        {
            _routes = _routes.Include(r => r.StartCity)
                .Include(r => r.FinishCity);
            return this;
        }

        public IQueryable<Route> Build()
        {
            var routes = _routes;
            _routes = _db.Routes;
            return routes;
        }
    }
}