using MediatR;
using ToDo.Application.Models.Dtos;

namespace ToDo.Application.Handlers.ToDoLists.Commands.CreateToDoList;

public record CreateToDoListCommand : IRequest<ToDoListDto>
{
    public string Title { get; set; }

    public string Description { get; set; }
}