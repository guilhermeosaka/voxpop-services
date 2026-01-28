namespace Voxpop.Profile.Application.Interfaces;

public interface IRequestContext
{
    Guid? UserId { get; set; }
}