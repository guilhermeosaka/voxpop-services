using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Application.Services.UserFinders;

public class EmailUserFinder(IUserRepository userRepository) : IUserFinder
{
    public Task<User?> FindAsync(string email, CancellationToken ct = default) =>
        userRepository.FindByEmailAsync(email);
}