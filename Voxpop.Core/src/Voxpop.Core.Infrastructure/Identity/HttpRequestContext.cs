using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Voxpop.Core.Application.Interfaces;
using Voxpop.Core.Domain.Common;

namespace Voxpop.Core.Infrastructure.Identity;

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