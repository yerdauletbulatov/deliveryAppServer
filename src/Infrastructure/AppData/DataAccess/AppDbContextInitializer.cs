using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities.AppEntities;
using ApplicationCore.Entities.AppEntities.Cars;
using ApplicationCore.Entities.AppEntities.Orders;
using ApplicationCore.Entities.AppEntities.Routes;
using ApplicationCore.Entities.Values.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;

namespace Infrastructure.AppData.DataAccess
{
    public class AppDbContextInitializer
    {
        public static void SeedAsync(AppDbContext context, ILogger<AppDbContextInitializer> logger) =>
            Policy.Handle<PostgresException>()
                .WaitAndRetry( new[]
                    {
                        TimeSpan.FromSeconds(4), 
                        TimeSpan.FromSeconds(3), 
                        TimeSpan.FromSeconds(2)
                    },
                    (ex, span) =>
                    {
                        logger.LogWarning("Failed! Waiting {0}", span);
                        logger.LogWarning("Error was {0}", ex.GetType().Name);
                        logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(AppDbContext));
                    })
                .Execute( () => InvokeSeeder(context).Wait());
        

        private static async Task InvokeSeeder(AppDbContext context)
        {
            if (!await context.Cities.AnyAsync())
            {
                await context.Cities.AddRangeAsync(GetPreconfiguredCities());
            }

            if (!await context.Routes.AnyAsync())
            {
                await context.Routes.AddRangeAsync(GetPreconfiguredRoutes());
            }

            if (!await context.RoutePrice.AnyAsync())
            {
                await context.RoutePrice.AddRangeAsync(GetPreconfiguredRoutePrice());
            }

            if (!await context.CarTypes.AnyAsync())
            {
                await context.CarTypes.AddRangeAsync(GetPreconfiguredCarTypes());
            }

            if (!await context.CarBrands.AnyAsync())
            {
                await context.CarBrands.AddRangeAsync(GetPreconfiguredCarBrands());
            }

            if (!await context.CarColors.AnyAsync())
            {
                await context.CarColors.AddRangeAsync(GetPreconfiguredCarColors());
            }

            if (!await context.Kits.AnyAsync())
            {
                await context.Kits.AddRangeAsync(GetPreconfiguredKits());
            }
            if (!await context.States.AnyAsync())
            {
                await context.States.AddRangeAsync(GetPreconfiguredStates());
            }
            await context.SaveChangesAsync();
        }

        private static IEnumerable<City> GetPreconfiguredCities() =>
            new List<City>
            {
                new(1, "????????????"),
                new(2, "??????????????"),
                new(3, "????????????")
            };

        private static IEnumerable<Route> GetPreconfiguredRoutes() =>
            new List<Route>
            {
                new(1, 1, 2),
                new(2, 1, 3),
                new(3, 2, 1),
                new(4, 3, 1),
                new(5, 2, 3),
                new(6, 3, 2)
            };

        private static IEnumerable<RoutePrice> GetPreconfiguredRoutePrice() =>
            new List<RoutePrice>
            {
                new(1, 1, 1000),
                new(2, 2, 2000),
                new(3, 3, 1000),
                new(4, 4, 2000),
                new(5, 5, 2000),
                new(6, 6, 2000)
            };

        private static IEnumerable<CarType> GetPreconfiguredCarTypes() =>
            new List<CarType>
            {
                new(1, "??????????"),
                new(2, "??????????????"),
                new(3, "????????????")
            };

        private static IEnumerable<CarBrand> GetPreconfiguredCarBrands() =>
            new List<CarBrand>
            {
                new(1, "BMW"),
                new(2, "Mercedes"),
                new(3, "Audi"),
                new(4, "Toyota"),
                new(5, "Subaru"),
                new(6, "Mitsubishi"),
                new(7, "Ford"),
                new(8, "Daweoo"),
                new(9, "Lada")
            };

        private static IEnumerable<CarColor> GetPreconfiguredCarColors() =>
            new List<CarColor>
            {
                new(1, "????????????"),
                new(2, "??????????"),
                new(3, "??????????"),
                new(4, "??????????????"),
                new(5, "????????????????"),
                new(6, "??????????????"),
                new(7, "??????????"),
                new(8, "????????????????????")
            };

        private static IEnumerable<Kit> GetPreconfiguredKits() =>
            new List<Kit>
            {
                new(1, "Light", 5, false),
                new(2, "Standard ", 10, false),
                new(3, "Premium", 15, false),
                new(4, "Unlimited", 999999, true)
            };
        private static IEnumerable<State> GetPreconfiguredStates() =>
            new List<State>
            {
                new((int)GeneralState.New, "??????????", ""),
                new((int)GeneralState.Waiting, "?????????????? ????????????????????????", ""),
                new((int)GeneralState.OnReview, "???? ????????????????????????", ""),
                new((int)GeneralState.PendingForHandOver, "?????????????? ????????????????", ""),
                new((int)GeneralState.ReceivedByDriver, "??????????????", ""),
                new((int)GeneralState.InProgress, "?? ????????", ""),
                new((int)GeneralState.Done, "", ""),
                new((int)GeneralState.Delayed, "", ""),
                new((int)GeneralState.Canceled, "", "")
            };
        
    }
}