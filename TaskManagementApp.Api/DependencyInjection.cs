using Microsoft.Extensions.Options;
using TaskManagementApp.Api.Extensions;

namespace TaskManagementApp.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDecisionEngineApiServices(this IServiceCollection services, IConfiguration configuration, ILoggerFactory loggerFactory, IWebHostEnvironment environment)
    {
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
            .AddAutoMapper(typeof(Program).Assembly)
            .AddValidation()
            //.AddSecurity(configuration)
            //.AddApiConfiguration(configuration)
            //.AddMongoDbServices(configuration, loggerFactory)
            //.AddInfrastructure(configuration)
            //.AddApplication()
            //
            //.AddMassTransitConfiguration(configuration, environment)
            ;

        services.Configure<RabbitMqOptions>(configuration.GetSection(RabbitMqOptions.SectionName));


        //services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}
