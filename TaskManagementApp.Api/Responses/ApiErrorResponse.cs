namespace TaskManagementApp.Api.Responses;
public class ApiErrorResponse(int statusCode, List<string> errors)
{
    public int StatusCode { get; } = statusCode;
    public List<string> Errors { get; } = errors;
}
