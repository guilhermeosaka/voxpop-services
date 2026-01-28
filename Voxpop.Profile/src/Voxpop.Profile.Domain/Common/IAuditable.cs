namespace Voxpop.Profile.Domain.Common;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime ModifiedAt { get; set; }
    
    bool IsArchived { get; set; }
    DateTime? ArchivedAt { get; set; } 
}