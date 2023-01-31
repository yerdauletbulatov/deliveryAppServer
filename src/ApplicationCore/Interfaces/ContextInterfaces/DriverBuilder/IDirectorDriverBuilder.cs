using System.Linq;
using ApplicationCore.Entities.AppEntities;

namespace ApplicationCore.Interfaces.ContextInterfaces.DriverBuilder
{
    public interface IDirectorDriverBuilder
    {
        public IQueryable<Driver> IncludeCarBuilder();
    }
}