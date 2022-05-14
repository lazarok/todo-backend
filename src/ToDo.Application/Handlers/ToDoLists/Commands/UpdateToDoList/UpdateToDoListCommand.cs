using MediatR;
using ToDo.Application.Models.Dtos;

namespace ToDo.Application.Handlers.ToDoLists.Commands.UpdateToDoList;

public class UpdateToDoListCommand : IRequest<ToDoListDto>
{
    public Guid Id { get; set; }
    public string? Title { get; set; }

    public string? Description { get; set; }
}