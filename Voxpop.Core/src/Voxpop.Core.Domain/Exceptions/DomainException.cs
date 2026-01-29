namespace Voxpop.Core.Domain.Exceptions;

public abstract class DomainException(string title, string message) : Exception(message)
{
    public string Title { get; private set; } = title;
}