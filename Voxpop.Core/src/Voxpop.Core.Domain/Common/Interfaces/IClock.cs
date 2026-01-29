namespace Voxpop.Core.Domain.Common.Interfaces;

public interface IClock
{
    DateTimeOffset UtcNow { get; }
}