using Voxpop.Identity.Application.Interfaces;

namespace Voxpop.Identity.Application.Services.CodeSender;

public class EmailCodeSender : ICodeSender
{
    public Task SendAsync(string target, string code)
    {
        throw new NotImplementedException($"Email sending is not implemented yet. (Target: {target}, Code: {code}).");
    }
}