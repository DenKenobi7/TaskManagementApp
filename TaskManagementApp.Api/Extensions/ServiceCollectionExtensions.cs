using MassTransit;
using MassTransit.Configuration;
using TaskManagementApp.Api.Consumers;
using TaskManagementApp.Application.Constants;
using TaskManagementApp.Application.Exceptions;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Infrastructure.Persistence;
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
            x.AddEntityFrameworkOutbox<AppDbContext>(o =>
            {
                o.UseSqlServer();

                o.UseBusOutbox();
            });

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

            x.AddConfigureEndpointsCallback((context, name, cfg) =>
            {
                cfg.UseEntityFrameworkOutbox<AppDbContext>(context);
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
                    e.ConfigureConsumer<UpdateTaskStatusActionConsumer>(context, c => c.UseMessageRetry(r =>
                    {
                        r.Exponential(3, 
                            TimeSpan.FromSeconds(1), 
                            TimeSpan.FromSeconds(30), 
                            TimeSpan.FromSeconds(5));
                        r.Ignore<NotFoundException>();
                        r.Ignore<InvalidOperationException>();
                    }));
                });

                cfg.ReceiveEndpoint(ServiceBusConstants.QueueNames.TaskActionCompletedEventQueue, e =>
                {
                    e.ConfigureConsumer<TaskActionCompletedEventConsumer>(context, c => c.UseMessageRetry(r =>
                    {
                        r.Exponential(3,
                            TimeSpan.FromSeconds(1),
                            TimeSpan.FromSeconds(30),
                            TimeSpan.FromSeconds(5));
                    }));
                });
            });
        });

        services.AddScoped<IServiceBusSender, ServiceBusSender>();

        return services;
    }
}