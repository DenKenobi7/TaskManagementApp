using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementApp.Application.Handlers.Commands.AddTask;
using TaskManagementApp.Application.Validation;

namespace TaskManagementApp.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AddTaskCommand>, AddTaskCommandValidator>();

        ValidatorOptions.Global.LanguageManager.Enabled = false;

        return services;
    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            })
            .AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);

        return services;
    }
}
