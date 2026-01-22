using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Domain.Interfaces;

public interface IUserRepository<TUser> where TUser : class
{
    Task AddAsync(User user);
    Task<TUser?> FindByPhoneNumberAsync(string phoneNumber);
    Task<bool> CheckPasswordAsync(TUser user, string password);
    Task<TUser?> FindByIdAsync(string userId);
    Task UpdateAsync(TUser user);
}