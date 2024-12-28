using FluentValidation;
using MediatR;

namespace TaskManagementApp.Application.Handlers.Commands.AddTask
{
    public class AddTaskCommandHandler() : IRequestHandler<AddTaskCommand, int>
    {
        public Task<int> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
