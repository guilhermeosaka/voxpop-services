using Voxpop.Core.Application.UserProfiles.Dtos;

namespace Voxpop.Core.Application.UserProfiles.Interfaces;

public interface IUserProfileQueries
{
    Task<UserProfileDto?> GetByUserId(Guid userId, string language);
}