using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.DriverInterfaces;
using Ardalis.ApiEndpoints;
using Infrastructure.Config.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Endpoints.Delivery
{
    [Authorize]
    public class GetActiveOrdersForDriver : EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        private readonly IDriver _driver;

        public GetActiveOrdersForDriver(IDriver driver)
        {
            _driver = driver;
        }
        
        [HttpPost("api/driver/activeOrders")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default) => 
            await _driver.GetActiveOrdersForDriverAsync(HttpContext.Items["UserId"]?.ToString());
    }
}