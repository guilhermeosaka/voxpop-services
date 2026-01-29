using Voxpop.Core.Domain.Interfaces;

namespace Voxpop.Core.Infrastructure.Services;

public class SystemClock : IClock
{
    public DateTime UtcNow { get; } = DateTime.UtcNow;
}