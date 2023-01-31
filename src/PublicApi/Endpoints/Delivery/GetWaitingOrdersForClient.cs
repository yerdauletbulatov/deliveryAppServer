using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.ClientInterfaces;
using Ardalis.ApiEndpoints;
using Infrastructure.Config.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Endpoints.Delivery
{
    [Authorize]
    public class GetWaitingOrdersForClient : EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        private readonly IOrder _order;

        public GetWaitingOrdersForClient(IOrder order)
        {
            _order = order;
        }

        [HttpPost("api/client/waitingOrders")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            return await _order.GetWaitingOrdersAsync(HttpContext.Items["UserId"]?.ToString(), cancellationToken);
        }
    }
}