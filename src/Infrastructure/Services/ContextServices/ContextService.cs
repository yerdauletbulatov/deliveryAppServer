using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.ContextInterfaces.DeliveryBuilder;
using ApplicationCore.Interfaces.ContextInterfaces.DriverBuilder;
using ApplicationCore.Interfaces.ContextInterfaces.OrderBuilder;
using ApplicationCore.Interfaces.ContextInterfaces.RouteBuilder;
using ApplicationCore.Interfaces.ContextInterfaces.RouteTripBuilder;
using Infrastructure.AppData.DataAccess;
using Infrastructure.Services.BuilderServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ContextServices
{
    public class ContextService : IContext
    {
        private readonly AppDbContext _db;

        public ContextService(AppDbContext db)
        {
            _db = db;
        }
        
        public  IQueryable<T> GetAll<T>() where T : BaseEntity =>
            _db.Set<T>();

        public async Task<T> FindAsync<T>(int id) where T : BaseEntity =>
            await _db.Set<T>().FirstOrDefaultAsync(p => p.Id == id);
        public async Task<T> FindAsync<T>(Expression< Func<T, bool>> expression) where T : BaseEntity  =>
            await _db.Set<T>().FirstOrDefaultAsync(expression);

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity 
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
        }    
        public async Task AddAsync<T>(T entity) where T : BaseEntity 
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }     
        public async Task<bool> AnyAsync<T>(Expression< Func<T, bool>> expression) where T : BaseEntity  =>
            await _db.Set<T>().AnyAsync(expression); 
        
        public async Task RemoveAsync<T>(T entity) where T : BaseEntity 
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        } 

        public IDirectorOrderBuilder Orders() => 
            new DirectorOrderBuilder(new OrderBuilder(_db));

        public IDirectorRouteTripBuilder RouteTrips() =>
            new DirectorRouteTripBuilder(new RouteTripBuilder(_db));     
        
        public IDirectorRouteBuilder Routes() =>
            new DirectorRouteBuilder(new RouteBuilder(_db));        
        
        public IDirectorDeliveryBuilder Deliveries() =>
            new DirectorDeliveryBuilder(new DeliveryBuilder(_db));
        
        public IDirectorDriverBuilder Drivers() =>
            new DirectorDriverBuilder(new DriverBuilder(_db));
    }
}