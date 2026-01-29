namespace Voxpop.Core.Domain.Common;

public abstract class AuditableEntity(Guid id) : Entity(id), IAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsArchived { get; set; }
    public DateTime? ArchivedAt { get; set; }
}