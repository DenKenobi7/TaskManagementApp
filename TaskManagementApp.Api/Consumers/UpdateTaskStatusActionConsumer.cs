using MassTransit;
using MassTransit.Mediator;
using TaskManagementApp.Application.Actions;
using TaskManagementApp.Application.Handlers.Commands.UpdateTaskStatus;

namespace TaskManagementApp.Api.Consumers;

public class UpdateTaskStatusActionConsumer(IMediator mediator, 
    ILogger<UpdateTaskStatusActionConsumer> logger)
: IConsumer<UpdateTestStatusAction>
{
    public async Task Consume(ConsumeContext<UpdateTestStatusAction> context)
    {
        try
        {
            await mediator.Send(new UpdateTaskStatusCommand(context.Message.Id, context.Message.NewStatus));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "UpdateTaskStatusActionConsumer: Unable to update task status. TaskId: {0}.",
                context.Message.Id);
        }
    }
}