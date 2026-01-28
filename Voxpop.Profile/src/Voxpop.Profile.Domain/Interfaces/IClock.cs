namespace Voxpop.Profile.Domain.Interfaces;

public interface IClock
{
    DateTime UtcNow { get; }
}