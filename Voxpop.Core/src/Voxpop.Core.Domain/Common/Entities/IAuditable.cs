namespace Voxpop.Core.Domain.Common.Entities;

public interface IAuditable
{
    Guid CreatedBy { get; set; }
    DateTimeOffset CreatedAt { get; set; }
    Guid ModifiedBy { get; set; }
    DateTimeOffset ModifiedAt { get; set; }
}