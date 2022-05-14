using FluentValidation;

namespace ToDo.Application.Handlers.ToDoLists.Commands.DeleteToDoList;

public class DeleteToDoListCommandValidator : AbstractValidator<DeleteToDoListCommand>
{
    public DeleteToDoListCommandValidator()
    {
        RuleFor(tdl => tdl.Id)
            .NotNull().WithMessage("'{PropertyName}' is required.")
            .NotEmpty().WithMessage("'{PropertyName}' is empty.");
    }
}