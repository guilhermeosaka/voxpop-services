using Voxpop.Core.Domain.Common.Interfaces;

namespace Voxpop.Core.Infrastructure.Services;

public class SystemClock : IClock
{
    public DateTimeOffset UtcNow { get; } = DateTimeOffset.UtcNow;
}