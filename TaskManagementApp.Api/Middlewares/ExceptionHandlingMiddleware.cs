using System.Text.Json;
using TaskManagementApp.Api.Responses;
using TaskManagementApp.Application.Exceptions;

namespace TaskManagementApp.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = ex switch
        {
            ValidationException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,
        };

        var errors = new List<string>();

        if (ex is ValidationException validationException)
        {
            errors.AddRange(validationException.Errors);
        } else
        {
            errors.Add(ex.Message);
        }

        var json = JsonSerializer.Serialize(
            new ApiErrorResponse(httpContext.Response.StatusCode, errors),
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        );

        await httpContext.Response.WriteAsync(json);
    }
}
