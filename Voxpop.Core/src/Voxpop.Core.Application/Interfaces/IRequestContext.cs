namespace Voxpop.Core.Application.Interfaces;

public interface IRequestContext
{
    Guid? UserId { get; set; }
    string Language { get; set; }
}