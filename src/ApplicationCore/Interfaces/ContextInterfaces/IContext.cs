using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.ContextInterfaces.DeliveryBuilder;
using ApplicationCore.Interfaces.ContextInterfaces.DriverBuilder;
using ApplicationCore.Interfaces.ContextInterfaces.OrderBuilder;
using ApplicationCore.Interfaces.ContextInterfaces.RouteBuilder;
using ApplicationCore.Interfaces.ContextInterfaces.RouteTripBuilder;

namespace ApplicationCore.Interfaces.ContextInterfaces
{
    public interface IContext
    {
        public IQueryable<T> GetAll<T>() where T : BaseEntity;
        public Task<T> FindAsync<T>(int id) where T : BaseEntity;
        public Task<T> FindAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
        public Task<bool> AnyAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;
        public Task UpdateAsync<T>(T entity) where T : BaseEntity;
        public Task AddAsync<T>(T entity) where T : BaseEntity;
        public Task RemoveAsync<T>(T entity) where T : BaseEntity;
        public IDirectorOrderBuilder Orders();
        public IDirectorRouteTripBuilder RouteTrips();
        public IDirectorRouteBuilder Routes();
        public IDirectorDeliveryBuilder Deliveries();
        public IDirectorDriverBuilder Drivers();

    }
}