using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Voxpop.Profile.Application.Interfaces;

namespace Voxpop.Profile.Infrastructure.Identity;

public class HttpRequestContext : IRequestContext
{
    public HttpRequestContext(IHttpContextAccessor httpContextAccessor)
    {
        var claim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(claim, out var userId)) UserId = userId;
    }
    
    public Guid? UserId { get; set; }
}