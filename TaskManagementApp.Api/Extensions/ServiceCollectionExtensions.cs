using MassTransit;
using TaskManagementApp.Api.Consumers;
using TaskManagementApp.Application.Constants;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.ServiceBus;
using TaskManagementApp.ServiceBus.Options;

namespace TaskManagementApp.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rabbitMqOptionsOptions = configuration
            .GetRequiredSection(RabbitMqOptions.SectionName)
            .Get<RabbitMqOptions>() ?? new RabbitMqOptions();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<UpdateTaskStatusActionConsumer>()
                .Endpoint(e =>
                {
                    e.Name = ServiceBusConstants.QueueNames.PushTaskStatusUpdateQueue;
                });

            x.AddConsumer<TaskActionCompletedEventConsumer>()
                .Endpoint(e =>
                {
                    e.Name = ServiceBusConstants.QueueNames.TaskActionCompletedEventQueue;
                });

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(rabbitMqOptionsOptions.Host), h =>
                {
                    h.Username(rabbitMqOptionsOptions.Username);
                    h.Password(rabbitMqOptionsOptions.Password);
                });

                cfg.ReceiveEndpoint(ServiceBusConstants.QueueNames.PushTaskStatusUpdateQueue, e =>
                {
                    //e.UseRawJsonSerializer(RawSerializerOptions.All);
                    e.ConfigureConsumer<UpdateTaskStatusActionConsumer>(context);
                });

                cfg.ReceiveEndpoint(ServiceBusConstants.QueueNames.TaskActionCompletedEventQueue, e =>
                {
                    //e.UseRawJsonSerializer(RawSerializerOptions.All);
                    e.ConfigureConsumer<TaskActionCompletedEventConsumer>(context);
                });

                cfg.UseMessageRetry(retryConfig =>
                {
                    retryConfig.Exponential(3,
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(30),
                        TimeSpan.FromSeconds(5));
                });
            });
        });

        services.AddScoped<IServiceBusSender, ServiceBusSender>();

        return services;
    }
}