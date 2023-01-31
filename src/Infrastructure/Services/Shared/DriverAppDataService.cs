using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.AppEntities.Cars;
using ApplicationCore.Entities.AppEntities.Routes;
using ApplicationCore.Entities.Values;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.SharedInterfaces;
using Infrastructure.AppData.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services.Shared
{
    public class DriverAppDataService : IDeliveryAppData<DriverAppDataInfo>
    {
        private readonly IContext _context;

        public DriverAppDataService(IContext context)
        {
            _context = context;
        }
        public Task<ActionResult> SendDataAsync(CancellationToken cancellationToken)
        {
            var info = new DriverAppDataInfo
            {
                Cities = _context.GetAll<City>().ToList(),
                Kits = _context.GetAll<Kit>().ToList(),
                CarBrands = _context.GetAll<CarBrand>().ToList(),
                CarTypes = _context.GetAll<CarType>().ToList(),
                CarColors = _context.GetAll<CarColor>().ToList()
            };
            return Task.FromResult<ActionResult>(new OkObjectResult(info));
        }
    }
}