using MediatR;

namespace TaskManagementApp.Application.Handlers.Commands.AddTask;

public record AddTaskCommand : IRequest<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string? AssignedTo { get; set; }
};