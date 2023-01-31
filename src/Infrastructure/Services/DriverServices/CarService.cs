using System.Threading;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities.Cars;
using ApplicationCore.Entities.Values;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces.ContextInterfaces;
using ApplicationCore.Interfaces.DriverInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.DriverServices
{
    public class CarService: ICar
    {
        private readonly IContext _context;

        public CarService(IContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> CreateAsync(CarInfo info, string userId, CancellationToken cancellationToken)
        {
            try
            {
                var driver = await _context.Drivers().IncludeCarBuilder().FirstOrDefaultAsync(d => d.UserId == userId,cancellationToken);
                var carBrand = await _context.FindAsync<CarBrand>(b => b.Id == info.CarBrandId);
                var carColor = await _context.FindAsync<CarColor>(b => b.Id == info.CarColorId);
                var carType = await _context.FindAsync<CarType>(b => b.Id == info.CarTypeId);

                driver.Car = new Car(info.ProductionYear, info.RegistrationCertificate, info.LicensePlate)
                    { CarBrand = carBrand, CarColor = carColor, CarType = carType };

                await _context.UpdateAsync(driver);
                return new NoContentResult();
            }
            catch(CarExistsException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch
            {
                return new BadRequestObjectResult("Not correct data");
            }
        }
    }
}