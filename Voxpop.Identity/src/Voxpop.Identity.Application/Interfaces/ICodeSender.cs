namespace Voxpop.Identity.Application.Interfaces;

public interface ICodeSender
{
    Task SendAsync(string target, string code);
}