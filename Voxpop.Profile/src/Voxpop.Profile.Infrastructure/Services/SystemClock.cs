using Voxpop.Profile.Domain.Interfaces;

namespace Voxpop.Profile.Infrastructure.Services;

public class SystemClock : IClock
{
    public DateTime UtcNow { get; } = DateTime.UtcNow;
}