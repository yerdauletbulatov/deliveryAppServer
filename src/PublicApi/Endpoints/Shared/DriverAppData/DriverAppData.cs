using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.Values;
using ApplicationCore.Interfaces.SharedInterfaces;
using Ardalis.ApiEndpoints;
using Infrastructure.Config.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Endpoints.Shared.DriverAppData
{
    [Authorize]
    public class DriverAppData : EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        private readonly IDeliveryAppData<DriverAppDataInfo> _deliveryAppData;
        
        public DriverAppData(IDeliveryAppData<DriverAppDataInfo> deliveryAppData)
        {
            _deliveryAppData = deliveryAppData;
        }

        [HttpPost("api/driver/appData")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = new CancellationToken()) 
            => await _deliveryAppData.SendDataAsync(cancellationToken);
    }
}