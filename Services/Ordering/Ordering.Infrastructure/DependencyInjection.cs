using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (
                this IServiceCollection services, 
                IConfiguration config
            )
        {
            var connString = config.GetConnectionString("Database");
            // add services
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            // register EF Core
            services.AddDbContext<ApplicationDbContext>((servProvider, opts) =>
            {
                opts.AddInterceptors(servProvider.GetServices<ISaveChangesInterceptor>());
                opts.UseSqlServer(connString);
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
