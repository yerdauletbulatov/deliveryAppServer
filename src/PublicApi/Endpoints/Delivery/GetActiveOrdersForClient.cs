using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.DeliveryInterfaces;
using Ardalis.ApiEndpoints;
using Infrastructure.Config.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Endpoints.Delivery
{
    [Authorize]
    public class GetActiveOrdersForClient: EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        private readonly IDelivery _delivery;

        public GetActiveOrdersForClient(IDelivery delivery)
        {
            _delivery = delivery;
        }

        [HttpPost("api/client/activeOrders")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default) =>
            await _delivery.GetActiveOrdersForClientAsync(HttpContext.Items["UserId"]?.ToString());
    }

}