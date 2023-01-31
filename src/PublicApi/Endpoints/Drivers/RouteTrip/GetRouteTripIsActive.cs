using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.DriverInterfaces;
using Ardalis.ApiEndpoints;
using Infrastructure.Config.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Endpoints.Drivers.RouteTrip
{
    [Authorize]
    public class GetRouteTripIsActive : EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        private readonly IRouteTrip _routeTrip;

        public GetRouteTripIsActive(IRouteTrip routeTrip)
        {
            _routeTrip = routeTrip;
        }
        
        [HttpPost("api/driver/activeRouteTrip")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default) => 
            await _routeTrip.GetRouteTripIsActiveAsync(HttpContext.Items["UserId"]?.ToString());
    }
}