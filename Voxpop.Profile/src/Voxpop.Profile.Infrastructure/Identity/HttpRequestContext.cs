using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Voxpop.Profile.Application.Interfaces;
using Voxpop.Profile.Domain.Common;

namespace Voxpop.Profile.Infrastructure.Identity;

public class HttpRequestContext : IRequestContext
{
    public HttpRequestContext(IHttpContextAccessor httpContextAccessor)
    {
        var claim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (Guid.TryParse(claim, out var userId)) UserId = userId;

        var language = httpContextAccessor.HttpContext?.Request.Headers.AcceptLanguage.FirstOrDefault() ??
                       Constants.DefaultLanguage;
        if (language.Contains(','))
            language = language[..language.IndexOf(',')].ToLower();
        Language = language;
    }

    public Guid? UserId { get; set; }
    public required string Language { get; set; }
}