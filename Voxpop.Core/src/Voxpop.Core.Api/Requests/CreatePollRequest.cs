using System.ComponentModel.DataAnnotations;
using Voxpop.Core.Application.Polls.UseCases.CreatePoll;
using Voxpop.Core.Contracts.Enums;

namespace Voxpop.Core.Api.Requests;

public record CreatePollRequest(
    string Question,
    DateTimeOffset? ExpiresAt,
    [EnumDataType(typeof(VoteMode))]
    VoteMode VoteMode,
    [EnumDataType(typeof(PollAccess))]
    PollAccess Access,
    [EnumDataType(typeof(PollResultsAccess))]
    PollResultsAccess ResultsAccess,
    [EnumDataType(typeof(PollResultsVisibility))]
    PollResultsVisibility ResultsVisibility,
    IReadOnlyList<CreatePollOptionDto> Options);