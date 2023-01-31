using System.Linq;
using ApplicationCore.Entities.AppEntities;

namespace ApplicationCore.Interfaces.ContextInterfaces.DriverBuilder
{
    public interface IDriverBuilder
    {
        public IDriverBuilder IncludeCar();
        public IQueryable<Driver> Build();
    }
}