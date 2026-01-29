using Voxpop.Core.Domain.Common.Exceptions;

namespace Voxpop.Core.Domain.Polls.Exceptions;

public class ConflictPollOptionException(string value)
    : ConflictException("Duplicate options are not allowed", $"Option with value '{value}' already exists");