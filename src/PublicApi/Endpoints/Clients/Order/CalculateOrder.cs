using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.Values;
using ApplicationCore.Interfaces.ClientInterfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PublicApi.Commands;

namespace PublicApi.Endpoints.Clients.Order
{
    public class CalculateOrder : EndpointBaseAsync.WithRequest<CreateOrderCommand>.WithActionResult
    {
        private readonly ICalculate _calculate;
        private readonly IMapper _mapper;


        public CalculateOrder(ICalculate calculate, IMapper mapper)
        {
            _calculate = calculate;
            _mapper = mapper;
        }

        [HttpPost("api/client/calculate")]
        public override async Task<ActionResult> HandleAsync([FromBody]CreateOrderCommand request, CancellationToken cancellationToken = new CancellationToken())
        {
            return await _calculate.CalculateAsync(_mapper.Map<OrderInfo>(request), cancellationToken);
        }
    }
}