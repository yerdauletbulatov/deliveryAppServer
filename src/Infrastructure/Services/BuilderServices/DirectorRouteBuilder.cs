using System.Linq;
using ApplicationCore.Entities.AppEntities.Routes;
using ApplicationCore.Interfaces.ContextInterfaces.RouteBuilder;

namespace Infrastructure.Services.BuilderServices
{
    public class DirectorRouteBuilder : IDirectorRouteBuilder
    {
        private readonly IRouteBuilder _builder;

        public DirectorRouteBuilder(IRouteBuilder builder)
        {
            _builder = builder;
        }
        
        public IQueryable<Route> IncludeRouteBuilder() =>
            _builder.IncludeRoutes().Build();

    }
}