using Microsoft.Extensions.DependencyInjection;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.ServiceBus;

namespace TaskManagementApp.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IServiceBusSender, ServiceBusSender>();

            return services;
        }
    }
}
