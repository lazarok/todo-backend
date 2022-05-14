using ToDo.Domain.Common;

namespace ToDo.Domain.Entities;

public class ToDoList : AuditableBaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public ICollection<ToDoItem> Items { get; set; }
}