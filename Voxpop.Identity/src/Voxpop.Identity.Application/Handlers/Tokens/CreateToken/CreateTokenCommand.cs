using Voxpop.Identity.Domain.Enums;

namespace Voxpop.Identity.Application.Handlers.Tokens.CreateToken;

public record CreateTokenCommand(string Target, VerificationCodeChannel Channel, string Code);