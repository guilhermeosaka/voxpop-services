using Microsoft.AspNetCore.Identity;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Infrastructure.Services;

public class Hasher(IPasswordHasher<VerificationCode> passwordHasher) : IHasher
{
    public string Hash(string plainText) => passwordHasher.HashPassword(null!, plainText);

    public bool Verify(string hashed, string plainText) =>
        passwordHasher.VerifyHashedPassword(null!, hashed, plainText) == PasswordVerificationResult.Success;
}