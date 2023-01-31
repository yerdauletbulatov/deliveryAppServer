using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Interfaces.ClientInterfaces;
using Ardalis.ApiEndpoints;
using Infrastructure.Config.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Endpoints.Delivery
{
    [Authorize]
    public class GetOnReviewOrdersForClient : EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        private readonly IOrder _order;

        public GetOnReviewOrdersForClient(IOrder order)
        {
            _order = order;
        }

        [HttpPost("api/client/onReviewOrders")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)=>
            await _order.GetOnReviewOrdersAsync(HttpContext.Items["UserId"]?.ToString(), cancellationToken);
    }
}