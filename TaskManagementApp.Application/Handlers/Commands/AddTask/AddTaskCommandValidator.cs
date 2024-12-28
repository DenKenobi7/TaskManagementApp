using FluentValidation;

namespace TaskManagementApp.Application.Handlers.Commands.AddTask;

public class AddTaskCommandValidator : AbstractValidator<AddTaskCommand>
{
    public AddTaskCommandValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Continue;
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty");
    }
}