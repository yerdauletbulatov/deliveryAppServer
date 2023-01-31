using System.Threading.Tasks;
using Infrastructure.AppData.DataAccess;
using Infrastructure.AppData.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using PublicApi.Extensions;

namespace PublicApi
{
    public static class Program
    {
        public static async Task Main(string[] args) =>
            await CreateHostBuilder(args)
                .Build()
                .MigrateDbContext<AppDbContext>((context, provider) =>
                {
                    var logger = provider.GetService<ILogger<AppDbContextInitializer>>(); // ?? GetRequiredService
                    AppDbContextInitializer.SeedAsync(context, logger);
                })
                .MigrateDbContext<AppIdentityDbContext>((_, _) => {})
                .RunAsync();

        private static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}