using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Application.Handlers.Commands.AddTask;
using TaskManagementApp.Application.Handlers.Commands.SendUpdateTaskStatusCommand;
using TaskManagementApp.Application.Handlers.Queries.GetAllTasks;
using TaskStatus = TaskManagementApp.Domain.TaskStatus;

namespace TaskManagementApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] AddTaskCommand newTask, CancellationToken cancellationToken)
    {
        var taskId = await mediator.Send(newTask, cancellationToken);
        return Created($"/api/tasks/{taskId}", new { Id = taskId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks(CancellationToken cancellationToken)
    {
        var tasks = await mediator.Send(new GetAllTasksQuery(), cancellationToken);
        return Ok(tasks);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] TaskStatus newStatus, CancellationToken cancellationToken)
    {
        await mediator.Send(new SendUpdateTaskStatusCommand(id, newStatus), cancellationToken);
        return Ok();
    }
}