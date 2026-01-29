namespace Voxpop.Core.Domain.Interfaces;

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}