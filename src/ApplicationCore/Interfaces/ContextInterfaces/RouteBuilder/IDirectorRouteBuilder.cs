using System.Linq;
using ApplicationCore.Entities.AppEntities.Routes;

namespace ApplicationCore.Interfaces.ContextInterfaces.RouteBuilder
{
    public interface IDirectorRouteBuilder
    {
        public IQueryable<Route> IncludeRouteBuilder();

    }
}