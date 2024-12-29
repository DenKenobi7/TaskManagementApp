using MediatR;
using TaskManagementApp.Application.Interfaces;
using TaskManagementApp.Domain;

namespace TaskManagementApp.Application.Handlers.Commands.AddTask
{
    public class AddTaskCommandHandler(ITaskRepository repository,
        IUnitOfWork unitOfWork) : IRequestHandler<AddTaskCommand, int>
    {
        public async Task<int> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new TaskEntity(request.Name, request.Description, request.AssignedTo);
            await repository.AddAsync(task, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return task.ID;
        }
    }
}
