using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Application.Services.UserFinder;

public class EmailUserFinder(IUserRepository userRepository) : IUserFinder
{
    public Task<User?> FindAsync(string email) => userRepository.FindByEmailAsync(email);
}