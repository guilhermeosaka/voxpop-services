using Voxpop.Identity.Domain.Enums;

namespace Voxpop.Identity.Application.Handlers.Verifications.CreateCode;

public record CreateCodeCommand(string Target, VerificationCodeChannel Channel);