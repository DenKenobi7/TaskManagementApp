using AutoMapper;
using MediatR;
using TaskManagementApp.Application.DTOs;
using TaskManagementApp.Application.Interfaces;

namespace TaskManagementApp.Application.Handlers.Queries.GetAllTasks;

public class GetAllTasksQueryHandler(ITaskRepository repository, IMapper mapper) : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskEntityDto>>
{
    public async Task<IEnumerable<TaskEntityDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<TaskEntityDto>>(await repository.GetAllAsync(cancellationToken));
    }
}