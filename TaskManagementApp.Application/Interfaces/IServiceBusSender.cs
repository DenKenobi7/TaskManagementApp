namespace TaskManagementApp.Application.Interfaces;

public interface IServiceBusSender
{
    Task SendAsync<T>(T message, string queueName, CancellationToken cancellationToken) where T : class;
}