namespace Voxpop.Core.Domain.Common;

public abstract class AuditableEntity(Guid id) : Entity(id), IAuditable
{
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public bool IsArchived { get; set; }
    public Guid? ArchivedBy { get; set; }
    public DateTimeOffset? ArchivedAt { get; set; }
}