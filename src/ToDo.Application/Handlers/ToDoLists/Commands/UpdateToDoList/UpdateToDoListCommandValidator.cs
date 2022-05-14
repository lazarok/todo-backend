using FluentValidation;

namespace ToDo.Application.Handlers.ToDoLists.Commands.UpdateToDoList;

public class UpdateToDoListCommandValidator : AbstractValidator<UpdateToDoListCommand>
{
    public UpdateToDoListCommandValidator()
    {
        RuleFor(tdl => tdl.Id)
            .NotNull().WithMessage("'{PropertyName}' is required.")
            .NotEmpty().WithMessage("'{PropertyName}' is empty.");
            
        RuleFor(tdl => tdl.Title)
            .MinimumLength(5).WithMessage("'{PropertyName}' must exceed 5 characters.")
            .MaximumLength(20).WithMessage("'{PropertyName}' must not exceed 20 characters.");
        
        RuleFor(tdl => tdl.Description)
            .MaximumLength(50).WithMessage("'{PropertyName}' must not exceed 50 characters.");
    }
}