namespace Voxpop.Core.Domain.Exceptions;

public class ConflictPollOptionException(string value)
    : ConflictException("Duplicate options are not allowed", $"Option with value '{value}' already exists");