namespace TaskManagementApp.Application.Exceptions
{
    public class ValidationException(List<string> errors) : Exception
    {
        public List<string> Errors { get; } = errors;
    }
}
