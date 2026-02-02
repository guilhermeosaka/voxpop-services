using Voxpop.Core.Application.Profiles.Models;

namespace Voxpop.Core.Application.Profiles.Queries;

public interface IProfileQueries
{
    Task<ProfileSummary?> GetByUserIdAsync(Guid userId, string language);
}