using Voxpop.Identity.Domain.Enums;

namespace Voxpop.Identity.Api.Dtos;

public record CreateCodeRequest(string Target, VerificationCodeChannel Channel);