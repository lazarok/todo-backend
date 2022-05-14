using ToDo.Domain.Enums;

namespace ToDo.Application.Handlers.ToDoItems.Queries.GetToDoItemById;

public record GetToDoItemByIdDto
{
    public string Title { get; set; }
    public string Note { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime? Scheduled { get; set; }
    public bool Done { get; set; }
    
    public Guid ToDoListId { get; set; }
}