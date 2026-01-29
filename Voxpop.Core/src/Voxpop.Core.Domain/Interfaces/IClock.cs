namespace Voxpop.Core.Domain.Interfaces;

public interface IClock
{
    DateTime UtcNow { get; }
}