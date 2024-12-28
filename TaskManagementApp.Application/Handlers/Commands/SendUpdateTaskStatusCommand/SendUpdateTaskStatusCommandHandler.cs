using MediatR;
using TaskManagementApp.Application.Actions;
using TaskManagementApp.Application.Constants;
using TaskManagementApp.Application.Interfaces;

namespace TaskManagementApp.Application.Handlers.Commands.SendUpdateTaskStatusCommand;

public class SendUpdateTaskStatusCommandHandler(IServiceBusSender sender) : IRequestHandler<SendUpdateTaskStatusCommand>
{
    public async Task Handle(SendUpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        await sender.SendAsync(new UpdateTestStatusAction(request.Id, request.NewStatus),
            ServiceBusConstants.QueueNames.PushTaskStatusUpdateQueue, cancellationToken);
    }
}
