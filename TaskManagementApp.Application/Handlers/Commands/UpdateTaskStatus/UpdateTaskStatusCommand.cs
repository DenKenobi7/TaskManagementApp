using MediatR;
using TaskStatus = TaskManagementApp.Domain.TaskStatus;

namespace TaskManagementApp.Application.Handlers.Commands.UpdateTaskStatus;

public record UpdateTaskStatusCommand(int Id, TaskStatus NewStatus) : IRequest;