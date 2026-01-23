using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Application.Interfaces;

public interface IUserFinder
{
    Task<User?> FindAsync(string target);
}