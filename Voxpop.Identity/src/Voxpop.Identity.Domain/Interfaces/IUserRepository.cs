using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> FindByPhoneNumberAsync(string phoneNumber);
    Task<User?> FindByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<User?> FindByIdAsync(string userId);
    Task<User?> FindByIdAsync(Guid userId);
}