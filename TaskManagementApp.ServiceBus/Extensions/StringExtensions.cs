namespace TaskManagementApp.ServiceBus.Extensions;

public static class StringExtensions
{
    public static Uri ToQueueUri(this string value)
    {
        return new Uri($"queue:{value}");
    }
}
