namespace Voxpop.Core.Application.Common.Interfaces;

public interface IRequestContext
{
    Guid? UserId { get; set; }
    string Language { get; set; }
}