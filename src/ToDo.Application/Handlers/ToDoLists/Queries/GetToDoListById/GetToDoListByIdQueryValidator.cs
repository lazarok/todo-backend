using FluentValidation;

namespace ToDo.Application.Handlers.ToDoLists.Queries.GetToDoListById;

public class GetToDoListByIdQueryValidator : AbstractValidator<GetToDoListByIdQuery>
{
    public GetToDoListByIdQueryValidator()
    {
        RuleFor(d => d.Id)
            .NotNull().WithMessage("'{PropertyName}' is required.")
            .NotEmpty().WithMessage("'{PropertyName}' is empty.");
    }
}