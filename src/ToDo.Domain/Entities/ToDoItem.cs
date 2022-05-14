using ToDo.Domain.Common;
using ToDo.Domain.Enums;

namespace ToDo.Domain.Entities;

public class ToDoItem : AuditableBaseEntity
{
    public string Title { get; set; }
    public string Note { get; set; }
    public PriorityLevel Priority { get; set; }
    public DateTime? Scheduled { get; set; }
    public bool Done { get; set; }
    
    public Guid ToDoListId { get; set; }
    public ToDoList ToDoList { get; set; }
}