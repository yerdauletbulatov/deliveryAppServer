using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.Values;
using ApplicationCore.Interfaces.DriverInterfaces;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Infrastructure.Config.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Endpoints.Drivers.Car
{
    [Authorize]
    public class CreateCar: EndpointBaseAsync.WithRequest<CarCommand>.WithActionResult
    {
        private readonly IMapper _mapper;
        private readonly ICar _car;

        public CreateCar(IMapper mapper, ICar car)
        {
            _mapper = mapper;
            _car = car;
        }

        [HttpPost("api/driver/createCar")]
        public override async Task<ActionResult> HandleAsync([FromBody]CarCommand request,
            CancellationToken cancellationToken = new CancellationToken())
        {
                return await _car.CreateAsync(_mapper.Map<CarInfo>(request),
                    HttpContext.Items["UserId"]?.ToString(), cancellationToken);
        }
    }
}