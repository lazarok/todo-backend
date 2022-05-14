using MediatR;
using ToDo.Application.Models.Dtos;

namespace ToDo.Application.Handlers.ToDoLists.Queries.GetToDoListById;

public record GetToDoListByIdQuery : IRequest<ToDoListDto>
{
    public Guid Id { get; set; }
}