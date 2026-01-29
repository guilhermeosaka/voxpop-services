using Voxpop.Core.Domain.Common.Entities;
using Voxpop.Core.Domain.Common.Interfaces;
using Voxpop.Core.Domain.Polls.Exceptions;

namespace Voxpop.Core.Domain.Polls.Entities;

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