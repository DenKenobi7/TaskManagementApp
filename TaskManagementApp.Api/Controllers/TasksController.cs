using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskStatus = TaskManagementApp.Domain.TaskStatus;

namespace TaskManagementApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] AddTaskDto task)
    {
        var taskId = await mediator.Send(task);
        return Created($"/api/tasks/{taskId}", new { Id = taskId });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await mediator.Send(new GetAllTasksQuery());
        return Ok(tasks);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] TaskStatus newStatus)
    {
        var updateResult = await mediator.Send(new UpdateTaskStatusCommand
        {
            Id = id, 
            NewStatus = newStatus 
        });
        return updateResult.IsSuccessfull 
            ? Ok() 
            : BadRequest(updateResult.ErrorMessage);
    }
}