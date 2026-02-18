using Voxpop.Core.Contracts.Enums;
using Voxpop.Core.Domain.Common.Entities;
using Voxpop.Core.Domain.Common.Interfaces;
using Voxpop.Core.Domain.Polls.Exceptions;

namespace Voxpop.Core.Domain.Polls.Entities;

public class Poll(
    Guid id, 
    string question, 
    DateTimeOffset? expiresAt, 
    VoteMode voteMode,
    PollAccess access,
    PollResultsAccess resultsAccess,
    PollResultsVisibility resultsVisibility)
    : Entity(id), IAuditable, IArchivable, IAggregateRoot
{
    private readonly List<PollOption> _options = [];
    
    public Guid CreatedBy { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid ModifiedBy { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public bool IsArchived { get; set; }
    public Guid? ArchivedBy { get; set; }
    public DateTimeOffset? ArchivedAt { get; set; }

    public string Question { get; private set; } = question;
    public DateTimeOffset? ExpiresAt { get; private set; } = expiresAt;
    public VoteMode VoteMode { get; private set; } = voteMode;
    public bool IsClosed { get; private set; }
    public PollAccess Access { get; private set; } = access;
    public PollResultsAccess ResultsAccess { get; private set; } = resultsAccess;
    public PollResultsVisibility ResultsVisibility { get; private set; } = resultsVisibility;
    
    public IReadOnlyCollection<PollOption> Options => _options;

    public static Poll Create(
        string question, 
        DateTimeOffset? expiresAt, 
        VoteMode voteMode,
        PollAccess access,
        PollResultsAccess resultsAccess,
        PollResultsVisibility resultsVisibility) =>
        new(Guid.NewGuid(), question, expiresAt, voteMode, access, resultsAccess, resultsVisibility);

    public void AddOption(string value)
    {
        if (_options.Any(o => o.Value.Equals(value, StringComparison.OrdinalIgnoreCase)))
            throw new ConflictPollOptionException(value);

        _options.Add(PollOption.Create(Id, Options.Count, value));
    }
    
    public void Close() => IsClosed = true;
}