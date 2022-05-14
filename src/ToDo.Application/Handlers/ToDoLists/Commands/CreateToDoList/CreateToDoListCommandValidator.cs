using FluentValidation;
using ToDo.Application.Interfaces;

namespace ToDo.Application.Handlers.ToDoLists.Commands.CreateToDoList;

public class CreateToDoListCommandValidator : AbstractValidator<CreateToDoListCommand>
{
    private readonly IToDoUnitOfWork _unitOfWork;

    public CreateToDoListCommandValidator(IToDoUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(tdl => tdl.Title)
            .NotNull().WithMessage("'{PropertyName}' is required.")
            .NotEmpty().WithMessage("'{PropertyName}' is empty.")
            .MinimumLength(5).WithMessage("'{PropertyName}' must exceed 5 characters.")
            .MaximumLength(20).WithMessage("'{PropertyName}' must not exceed 20 characters.");

        RuleFor(tdl => tdl.Description)
            .MaximumLength(50).WithMessage("'{PropertyName}' must not exceed 50 characters.");

        RuleFor(tdl => tdl.Title)
            .MustAsync(NotExistsTitle).WithMessage("'{PropertyName}' exists.");
    }

    private async Task<bool> NotExistsTitle(string title, CancellationToken cancellationToken)
    {
        var toDoList = await _unitOfWork.ToDoListRepository.GetByCriteriaAsync(_ => _.Title == title, cancellationToken);
        return toDoList == null;
    }
}