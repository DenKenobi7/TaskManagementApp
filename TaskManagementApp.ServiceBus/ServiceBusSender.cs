using MassTransit;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.ServiceBus.Extensions;

namespace TaskManagementApp.ServiceBus;

public class ServiceBusSender(ISendEndpointProvider sendEndpointProvider)
    : IServiceBusSender
{
    public async Task SendAsync<T>(T message, string queueName, CancellationToken cancellationToken) where T : class
    {
        if (message == null)
        {
            return;
        }
        var endpoint = await sendEndpointProvider.GetSendEndpoint(queueName.ToQueueUri());
        
        await endpoint.Send(message, cancellationToken);
    }
}
