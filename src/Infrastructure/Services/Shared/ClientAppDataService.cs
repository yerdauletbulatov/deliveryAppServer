using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities.Cars;
using ApplicationCore.Entities.AppEntities.Routes;
using ApplicationCore.Entities.Values;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.SharedInterfaces;
using Infrastructure.AppData.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Shared
{
    public class ClientAppDataService : IDeliveryAppData<ClientAppDataInfo>
    {
        private readonly IContext _context;
        public ClientAppDataService(IContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> SendDataAsync(CancellationToken cancellationToken)
        {
            var cities = await _context.GetAll<City>().ToListAsync(cancellationToken);
            var carTypes = await _context.GetAll<CarType>().ToListAsync(cancellationToken);
            var info = new ClientAppDataInfo
            {
                Cities = cities,
                CarTypes = carTypes
            };
            return new OkObjectResult(info);
        }
    }
}