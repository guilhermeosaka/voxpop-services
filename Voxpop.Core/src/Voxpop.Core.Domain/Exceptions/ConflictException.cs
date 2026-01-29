namespace Voxpop.Core.Domain.Exceptions;

public class ConflictException(string title, string message) : DomainException(title, message);