using MediatR;
using TaskManagementApp.Application.DTOs;

namespace TaskManagementApp.Application.Handlers.Queries.GetAllTasks
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskEntityDto>>
    {
        public Task<IEnumerable<TaskEntityDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
