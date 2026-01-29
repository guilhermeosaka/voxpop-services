namespace Voxpop.Core.Domain.Common.Exceptions;

public class ConflictException(string title, string message) : DomainException(title, message);