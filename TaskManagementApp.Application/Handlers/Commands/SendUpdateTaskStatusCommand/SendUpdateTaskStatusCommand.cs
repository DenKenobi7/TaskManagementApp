using MediatR;
using TaskStatus = TaskManagementApp.Domain.TaskStatus;

namespace TaskManagementApp.Application.Handlers.Commands.SendUpdateTaskStatusCommand;

public record SendUpdateTaskStatusCommand(int Id, TaskStatus NewStatus) : IRequest;