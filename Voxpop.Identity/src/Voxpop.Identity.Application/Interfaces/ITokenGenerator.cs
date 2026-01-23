using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Application.Interfaces;

public interface ITokenGenerator
{
    string Generate(User user, string? audience = null);
}