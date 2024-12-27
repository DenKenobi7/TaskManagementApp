using FluentValidation;
using TaskManagementApp.Application.Handlers.Commands.AddTask;

namespace TaskManagementApp.Api.Validators;

public class AddTaskCommandValidator : AbstractValidator<AddTaskCommand>
{
    public AddTaskCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty");
    }
}