namespace TaskManagementApp.Application.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetTodayDateTimeUtc()
    {
        return DateTime.UtcNow;
    }
}