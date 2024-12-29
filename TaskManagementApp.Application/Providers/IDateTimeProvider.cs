namespace TaskManagementApp.Application.Providers;

public interface IDateTimeProvider
{
    public DateTime GetTodayDateTimeUtc();
}