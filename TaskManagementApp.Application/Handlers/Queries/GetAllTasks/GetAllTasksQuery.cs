using MediatR;
using TaskManagementApp.Application.DTOs;

namespace TaskManagementApp.Application.Handlers.Queries.GetAllTasks;

public record GetAllTasksQuery : IRequest<IEnumerable<TaskEntityDto>>;