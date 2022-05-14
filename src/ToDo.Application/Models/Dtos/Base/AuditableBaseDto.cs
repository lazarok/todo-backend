namespace ToDo.Application.Models;

public class AuditableBaseDto : BaseDto
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}