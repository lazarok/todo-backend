
namespace ToDo.Application.Models.Dtos;

public class ToDoListDto : AuditableBaseDto
{
    public string Title { get; set; }

    public string Description { get; set; }
}