using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities.Routes;
using ApplicationCore.Entities.Values;
using ApplicationCore.Interfaces.ClientInterfaces;
using ApplicationCore.Interfaces.ContextInterfaces;
using Infrastructure.AppData.DataAccess;
using Infrastructure.Services.ContextServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.ClientServices
{
    public class CalculateService : ICalculate
    {
        private decimal RoutePrice { get; set; }
        private double VolumeWeight { get; set; }
        
        private readonly IContext _context;

        public CalculateService(IContext context)
        {
            _context = context;
        }


        public async Task<ActionResult> CalculateAsync(OrderInfo info,CancellationToken cancellationToken)
        {
            VolumeWeight = info.Package.Length * info.Package.Width * info.Package.Height / 0.005;
            RoutePrice = await SetRoutePrice(info);
            var weight = VolumeWeight > info.Package.Weight ? VolumeWeight : info.Package.Weight;
            info.Price = AddPerKilo(RoutePrice, weight);
            if (info.Package.Weight > 50 || info.Package.Length > 1 || info.Package.Width > 1 || info.Package.Height > 1)
            {
                info.Price *= (decimal)1.5;
            }

            return  await Task.FromResult<ActionResult>(new OkObjectResult(info));
        }

        private decimal AddPerKilo(decimal price, double kilo)
        {
            return price <= 5 
                ? price 
                : price + 250 * (decimal)((int)kilo - 5);
        }

        private async Task<decimal> SetRoutePrice(OrderInfo info)
        {
            var route = await _context.FindAsync<RoutePrice>(r => r.Route.StartCityId == info.StartCity.Id &&
                                     r.Route.FinishCityId == info.FinishCity.Id);
            return route?.Price ?? 0;
        } 
    }
}