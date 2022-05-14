using MediatR;

namespace ToDo.Application.Handlers.ToDoItems.Queries.GetToDoItemById;

public record GetToDoItemByIdQuery(Guid Id) : IRequest<GetToDoItemByIdDto>;