using Microsoft.AspNetCore.Identity;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;
using Voxpop.Identity.Infrastructure.Persistence.Entities;

namespace Voxpop.Identity.Infrastructure.Persistence.Repositories;

public class UserRepository(UserManager<ApplicationUser> userManager) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        var applicationUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = user.PhoneNumber,
            PhoneNumber = user.PhoneNumber
        };

        await userManager.CreateAsync(applicationUser);
    }

    public async Task<User?> FindByPhoneNumberAsync(string phoneNumber)
    {
        var applicationUser = await userManager.FindByNameAsync(phoneNumber);
        return applicationUser == null
            ? null
            : new User(applicationUser.Id, applicationUser.UserName!, applicationUser.Email);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        var applicationUser = await userManager.FindByEmailAsync(email);
        return applicationUser == null
            ? null
            : new User(applicationUser.Id, applicationUser.UserName!, applicationUser.Email);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        var applicationUser = await userManager.FindByNameAsync(user.PhoneNumber);
        return applicationUser != null && await userManager.CheckPasswordAsync(applicationUser, password);
    }

    public async Task<User?> FindByIdAsync(string userId)
    {
        var applicationUser = await userManager.FindByIdAsync(userId);
        return applicationUser == null
            ? null
            : new User(applicationUser.Id, applicationUser.UserName!, applicationUser.Email);
    }

    public async Task<User?> FindByIdAsync(Guid userId) => await FindByIdAsync(userId.ToString());
}