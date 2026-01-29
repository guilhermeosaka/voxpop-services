using Voxpop.Core.Application.Profiles.Dtos;

namespace Voxpop.Core.Application.Profiles.Queries;

public interface IUserProfileQueries
{
    Task<UserProfileDto?> GetByUserIdAsync(Guid userId, string language);
}