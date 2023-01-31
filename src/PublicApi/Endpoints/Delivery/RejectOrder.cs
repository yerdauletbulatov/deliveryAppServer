using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PublicApi.Commands;
using PublicApi.Helpers;

namespace PublicApi.Endpoints.Delivery
{
    public class RejectOrder : EndpointBaseAsync.WithRequest<RejectedOrderCommand>.WithActionResult
    {
        private readonly IMediator _mediator;

        public RejectOrder(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/driver/rejectOrder")]
        public override async Task<ActionResult> HandleAsync([FromBody] RejectedOrderCommand request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await _mediator.Send(request, cancellationToken);
                return new NoContentResult();
            }
            catch
            {
                return new BadRequestResult();
            }
        }
    }
}