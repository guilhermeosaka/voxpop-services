namespace Voxpop.Core.Domain.Common;

public interface IAuditable
{
    Guid CreatedBy { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    Guid ModifiedBy { get; set; }
    DateTimeOffset ModifiedAt { get; set; }
    bool IsArchived { get; set; }
    Guid? ArchivedBy { get; set; }
    DateTimeOffset? ArchivedAt { get; set; } 
}