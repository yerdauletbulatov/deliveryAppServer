using System.Linq;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Interfaces.ContextInterfaces.DriverBuilder;
using ApplicationCore.Interfaces.DriverInterfaces;

namespace Infrastructure.Services.BuilderServices
{
    public class DirectorDriverBuilder : IDirectorDriverBuilder
    {
        private readonly IDriverBuilder _builder;

        public DirectorDriverBuilder(IDriverBuilder builder)
        {
            _builder = builder;
        }

        public IQueryable<Driver> IncludeCarBuilder() =>
            _builder.IncludeCar().Build();
    }
}