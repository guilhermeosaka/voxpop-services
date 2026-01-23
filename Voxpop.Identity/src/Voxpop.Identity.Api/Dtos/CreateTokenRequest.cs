using Voxpop.Identity.Domain.Enums;

namespace Voxpop.Identity.Api.Dtos;

public record CreateTokenRequest(string Target, VerificationCodeChannel Channel, string Code);