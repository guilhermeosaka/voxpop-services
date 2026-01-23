using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Options;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Infrastructure.Services;

public class JwtGenerator(IOptions<JwtOptions> options) : ITokenGenerator
{
    public string Generate(User user, string? audience = null)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber),
        };

        if (!string.IsNullOrEmpty(user.Email))
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: options.Value.Issuer,
            audience: audience ?? options.Value.Audiences[0],
            claims: claims,
            expires: DateTime.UtcNow + options.Value.ExpiresIn,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}