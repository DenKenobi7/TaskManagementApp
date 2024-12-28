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
            .AddInfrastructure()
            .AddMassTransitConfiguration(configuration)
            //.AddSecurity(configuration)
            //.AddApiConfiguration(configuration)
            //.AddMongoDbServices(configuration, loggerFactory)
            //.AddInfrastructure(configuration)
            //.AddApplication()
            //

            ;

        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));


        //services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}
