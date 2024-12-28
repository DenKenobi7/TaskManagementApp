using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApp.Application.Handlers.Commands.AddTask;
using TaskManagementApp.Application.Handlers.Commands.SendUpdateTaskStatusCommand;
using TaskManagementApp.Application.Handlers.Commands.UpdateTaskStatus;
using TaskManagementApp.Application.Handlers.Queries.GetAllTasks;
using TaskStatus = TaskManagementApp.Domain.TaskStatus;

namespace TaskManagementApp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(IMediator mediator, ILogger<TasksController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddTask([FromBody] AddTaskCommand newTask, CancellationToken cancellationToken)
    {
        //var validationResult = await ValidateLeadRequestAsync(validator, newTask, cancellationToken);

        //if (!validationResult.IsValid)
        //{
        //    validationResult.AddToModelState(ModelState);

        //    logger.LogError("Lead Request model contains validation errors. Further Lead Request processing is not possible.");

        //    return BadRequest(ModelState);
        //}

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

    #region PrivateMethods

    protected async Task<ValidationResult> ValidateLeadRequestAsync(IValidator<AddTaskCommand> validator, AddTaskCommand addTaskCommand, CancellationToken cancellationToken)
    {
        ValidationResult validationResult;

        try
        {
            validationResult = await validator.ValidateAsync(addTaskCommand, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Exception occured while validating the AddTaskCommand model. Further task processing is not possible.");

            throw;
        }

        return validationResult;
    }

    #endregion
}