using MediatR;

namespace ToDo.Application.Handlers.ToDoLists.Commands.DeleteToDoList;

public class DeleteToDoListCommand : IRequest
{
    public Guid Id { get; set; }
}