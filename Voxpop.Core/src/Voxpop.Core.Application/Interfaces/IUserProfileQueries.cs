using Voxpop.Core.Application.Dtos;

namespace Voxpop.Core.Application.Interfaces;

public interface IUserProfileQueries
{
    Task<UserProfileDto?> GetByUserId(Guid userId, string language);
}