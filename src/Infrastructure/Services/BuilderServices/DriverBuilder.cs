using System.Linq;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Interfaces.ContextInterfaces.DriverBuilder;
using Infrastructure.AppData.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.BuilderServices
{
    public class DriverBuilder : IDriverBuilder
    {
        private IQueryable<Driver> _drivers;
        private readonly AppDbContext _db;
        public DriverBuilder(AppDbContext db)
        {
            _db = db;
            _drivers = _db.Drivers;
        }
   
        public IDriverBuilder IncludeCar()
        {
            _drivers = _drivers.Include(d => d.Car);
            return this;
        }

        public IQueryable<Driver> Build()
        {
            var drivers = _drivers;
            _drivers = _db.Drivers;
            return drivers;
        }
    }
}