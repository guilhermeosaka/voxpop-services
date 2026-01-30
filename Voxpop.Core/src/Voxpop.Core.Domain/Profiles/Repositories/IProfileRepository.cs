using Voxpop.Core.Domain.Common.Interfaces;
using Voxpop.Core.Domain.Profiles.Entities;

namespace Voxpop.Core.Domain.Profiles.Repositories;

public interface IProfileRepository : IRepository<Profile>
{
    Task<Profile?> FindByUserIdAsync(Guid userId);
}