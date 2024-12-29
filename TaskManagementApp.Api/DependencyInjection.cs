using TaskManagementApp.Api.Extensions;
using TaskManagementApp.Application.Extensions;
using TaskManagementApp.Infrastructure.Extensions;
using TaskManagementApp.ServiceBus.Options;

namespace TaskManagementApp.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddTaskManagementAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddValidation()
            .AddApplication()
            .AddInfrastructure(configuration)
            .AddMassTransitConfiguration(configuration);

        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));

        return services;
    }
}
