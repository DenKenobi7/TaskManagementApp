using MediatR;
using TaskManagementApp.Application.Actions;
using TaskManagementApp.Application.Constants;
using TaskManagementApp.Application.Exceptions;
using TaskManagementApp.Application.Interfaces;

namespace TaskManagementApp.Application.Handlers.Commands.SendUpdateTaskStatusCommand;

public class SendUpdateTaskStatusCommandHandler(ITaskRepository repository, IServiceBusSender sender) : IRequestHandler<SendUpdateTaskStatusCommand>
{
    public async Task Handle(SendUpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (task is null)
            throw new NotFoundException($"Task with Id: {request.Id} was not found.");

        await sender.SendAsync(new UpdateTestStatusAction(request.Id, request.NewStatus),
            ServiceBusConstants.QueueNames.PushTaskStatusUpdateQueue, cancellationToken);
    }
}
