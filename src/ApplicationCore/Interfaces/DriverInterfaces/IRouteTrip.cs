using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.Values;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationCore.Interfaces.DriverInterfaces
{
    public interface IRouteTrip
    {
        public Task<ActionResult> GetRouteTripIsActiveAsync(string driverUserId);

        public Task<Delivery> CreateAsync(RouteTripInfo tripInfo, string userId);
    }
}