using Voxpop.Core.Domain.Common;
using Voxpop.Core.Domain.Exceptions;
using Voxpop.Core.Domain.Interfaces;

namespace Voxpop.Core.Domain.Polls;

public class Poll(Guid id, string question, DateTimeOffset? expiresAt) : AuditableEntity(id), IAggregateRoot
{
    private readonly List<PollOption> _options = [];
    
    public string Question { get; private set; } = question;
    public IReadOnlyCollection<PollOption> Options => _options;
    public DateTimeOffset? ExpiresAt { get; private set; } = expiresAt;

    public static Poll Create(string question, DateTimeOffset? expiresAt) => new(Guid.NewGuid(), question, expiresAt);
    
    public void AddOption(string value)
    {
        if (_options.Any(o => o.Value.Equals(value, StringComparison.OrdinalIgnoreCase)))
            throw new ConflictPollOptionException(value);
        
        _options.Add(PollOption.Create(Id, value));  
    } 
}