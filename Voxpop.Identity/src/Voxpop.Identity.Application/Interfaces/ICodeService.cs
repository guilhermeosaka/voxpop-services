namespace Voxpop.Identity.Application.Interfaces;

public interface ICodeService
{
    Task SendAsync(string target, CancellationToken ct = default);
    Task<bool> VerifyAsync(string target, string code, CancellationToken ct = default);
}