namespace CleanArchitectureTemplate.Domain.Common;

public abstract class BaseEntity : AuditableEntity
{
    public long Id { get; set; }
}
