using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities.Locations;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Interfaces.ContextInterfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PublicApi.Helpers;

namespace PublicApi.Commands
{
    public class LocationCommandHandler : AsyncRequestHandler<LocationCommand>
    {
        private readonly HubHelper _hubHelper;
        private readonly IContext _context;

        public LocationCommandHandler(HubHelper hubHelper, IContext context)
        {
            _hubHelper = hubHelper;
            _context = context;
        }

        protected override async Task Handle(LocationCommand request, CancellationToken cancellationToken)
        {
            var orders = await OrdersAsync(request.UserId);
            var locationCommand =  await CurrentLocationAsync(orders.FirstOrDefault()!.Delivery.RouteTrip.Id, request, cancellationToken);
            await _hubHelper.SendDriverLocationToClientsAsync(orders, locationCommand);
        }
        
        private async Task<LocationCommand> CurrentLocationAsync(int routeTripId, LocationCommand locationCommand, CancellationToken cancellationToken)
        {
            if (locationCommand.Latitude != 0 && locationCommand.Longitude != 0)
            {
                return locationCommand;
            }
            var locationDate = await _context.FindAsync<LocationDate>(l => l.RouteTrip.Id == routeTripId);
            locationCommand.Latitude = locationDate.Location.Latitude;
            locationCommand.Longitude = locationDate.Location.Longitude;
            return locationCommand;
        }
        
        private async Task<List<Order>> OrdersAsync(string userId) => 
            await _context
                .Orders()
                .IncludeDeliveriesInfoBuilder()
                .Where(o => o.Delivery.RouteTrip.Driver.UserId == userId)
                .ToListAsync();
    }
}