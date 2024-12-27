using FluentValidation;
using TaskManagementApp.Api.Validators;
using TaskManagementApp.Application.Handlers.Commands.AddTask;

namespace TaskManagementApp.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<AddTaskCommand>, AddTaskCommandValidator>();

        ValidatorOptions.Global.LanguageManager.Enabled = false;

        return services;
    }
}