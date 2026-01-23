using Voxpop.Identity.Domain.Enums;

namespace Voxpop.Identity.Application.Handlers.Codes.CreateCode;

public record CreateCodeCommand(string Target, VerificationCodeChannel Channel);