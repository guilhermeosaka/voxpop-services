using Voxpop.Identity.Domain.Enums;

namespace Voxpop.Identity.Application.Exceptions;

public class NotFoundException(string target, VerificationCodeChannel channel)
    : Exception($"User with {channel} '{target}' not found");