using ToDo.Domain.Enums;

namespace ToDo.Domain.Common;

public abstract class  AuditableBaseEntity : BaseEntity
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}