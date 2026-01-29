using Voxpop.Profile.Application.Dtos;

namespace Voxpop.Profile.Application.Interfaces;

public interface IUserProfileQueries
{
    Task<UserProfileDto?> GetByUserId(Guid userId, string language);
}