using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;

namespace PublicApi.Extensions
{
    public static class WebHostExtensions
    {
        private static bool IsInKubernetes(this IWebHost webHost)
        {
            var cfg = webHost.Services.GetService<IConfiguration>();
            var orchestratorType = cfg.GetValue<string>("OrchestratorType");
            return orchestratorType?.ToUpper() == "K8S";
        }

        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost,
            Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            var underK8S = webHost.IsInKubernetes();

            using var scope = webHost.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}",
                    typeof(TContext).Name);

                if (underK8S)
                {
                    InvokeSeeder(seeder, context, services);
                }
                else
                {
                    Policy.Handle<PostgresException>()
                        .WaitAndRetry(new[]
                            {
                                TimeSpan.FromSeconds(4),
                                TimeSpan.FromSeconds(3),
                                TimeSpan.FromSeconds(2)
                            },
                            (ex, span) =>
                            {
                                logger.LogWarning("Failed! Waiting {0}", span);
                                logger.LogWarning("Error was {0}", ex.GetType().Name);
                            })
                        .Execute(() => InvokeSeeder(seeder, context, services));
                }

                logger.LogInformation("Migrated database associated with context {DbContextName}",
                    typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}",
                    typeof(TContext).Name);
                if (underK8S)
                {
                    throw; // Rethrow under k8s because we rely on k8s to re-run the pod
                }
            }

            return webHost;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
            IServiceProvider services)
            where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}