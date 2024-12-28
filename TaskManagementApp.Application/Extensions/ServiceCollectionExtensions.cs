using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskManagementApp.Application.Handlers.Commands.AddTask;

namespace TaskManagementApp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly))
            .AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        return services;
    }
}
