using MediatR;
using TaskManagementApp.Application.Constants;
using TaskManagementApp.Application.Events;
using TaskManagementApp.Application.Exceptions;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Application.Providers;

namespace TaskManagementApp.Application.Handlers.Commands.UpdateTaskStatus;

public class UpdateTaskStatusCommandHandler(ITaskRepository repository, 
    IUnitOfWork unitOfWork, 
    IServiceBusSender sender,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<UpdateTaskStatusCommand>
{
    public async Task Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (task is null)
            throw new NotFoundException($"Task with Id: {request.Id} was not found.");

        var oldStatus = task.Status;
        if (task.TryUpdateStatus(request.NewStatus))
        {
            await repository.UpdateAsync(task, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await sender.SendAsync(new TaskStatusUpdatedEvent(task.ID, oldStatus, task.Status,
                dateTimeProvider.GetTodayDateTimeUtc()), ServiceBusConstants.QueueNames.TaskActionCompletedEventQueue, cancellationToken);
        }

        throw new InvalidOperationException($"Status of Task with ID {task.ID} is already {request.NewStatus}");
    }
}