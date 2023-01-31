using System.Linq;
using ApplicationCore.Entities.AppEntities.Routes;

namespace ApplicationCore.Interfaces.ContextInterfaces.RouteBuilder
{
    public interface IRouteBuilder
    {
        public IRouteBuilder IncludeRoutes();
        public IQueryable<Route> Build();
    }
}