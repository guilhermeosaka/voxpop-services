namespace Voxpop.Core.Domain.Common.Entities;

public interface IArchivable
{
    bool IsArchived { get; set; }
    Guid? ArchivedBy { get; set; }
    DateTimeOffset? ArchivedAt { get; set; }
}