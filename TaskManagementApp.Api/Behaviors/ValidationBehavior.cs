using FluentValidation;
using MediatR;

namespace TaskManagementApp.Api.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Any())
        {
            // Assuming TResponse is standardized for failure handling, e.g., Result<T> or IActionResult
            var errorResponse = (TResponse)Activator.CreateInstance(
                typeof(TResponse),
                new object[] { failures.ToList() }
            )!;

            return errorResponse;
        }

        return await next();
    }
}