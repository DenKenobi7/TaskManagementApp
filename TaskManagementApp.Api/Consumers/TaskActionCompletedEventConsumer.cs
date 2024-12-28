using MassTransit;
using TaskManagementApp.Application.Events;

namespace TaskManagementApp.Api.Consumers;

public class TaskActionCompletedEventConsumer(ILogger<TaskActionCompletedEventConsumer> logger)
    : IConsumer<TaskStatusUpdatedEvent>
{
    public Task Consume(ConsumeContext<TaskStatusUpdatedEvent> context)
    {
        logger.LogInformation($"Status of Task (id: {context.Message.Id}) has been changed from {context.Message.OldStatus} to {context.Message.NewStatus}");
        return Task.CompletedTask;
    }
}