using Voxpop.Identity.Application.Commands;
using Voxpop.Identity.Application.Exceptions;
using Voxpop.Identity.Application.Interfaces;
using Voxpop.Identity.Application.Services;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;

namespace Voxpop.Identity.Application.Handlers;

public class RegisterUserHandler(IUserRepository<User> userRepository, VerificationCodeService verificationCodeService) : IHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand request, CancellationToken ct)
    {
        var existingUser = await userRepository.FindByPhoneNumberAsync(request.PhoneNumber);
        
        if (existingUser is { PhoneNumberConfirmed: true }) 
            throw new UserAlreadyExistsException(request.PhoneNumber);

        if (existingUser == null)
            await userRepository.AddAsync(new User(request.PhoneNumber));

        await verificationCodeService.SendVerificationCodeAsync(request.PhoneNumber);
    }
}