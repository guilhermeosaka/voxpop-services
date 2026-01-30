using Voxpop.Core.Application.Profiles.Dtos;

namespace Voxpop.Core.Application.Profiles.Queries;

public interface IProfileQueries
{
    Task<ProfileDto?> GetByUserIdAsync(Guid userId, string language);
}