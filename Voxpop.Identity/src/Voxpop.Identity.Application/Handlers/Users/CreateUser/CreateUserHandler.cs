using Voxpop.Identity.Application.Exceptions;
using Voxpop.Identity.Application.Handlers.Verifications.CreateCode;
using Voxpop.Identity.Domain.Enums;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;
using Voxpop.Packages.Handler.Interfaces;

namespace Voxpop.Identity.Application.Handlers.Users.CreateUser;

public class CreateUserHandler(IUserRepository userRepository, IHandler handler)
    : IHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken ct)
    {
        var existingUser = await userRepository.FindByPhoneNumberAsync(request.PhoneNumber);

        if (existingUser is { PhoneNumberConfirmed: true })
            throw new UserAlreadyExistsException(request.PhoneNumber);

        if (existingUser == null)
            await userRepository.AddAsync(new User(request.PhoneNumber));

        await handler.Handle(new CreateCodeCommand(request.PhoneNumber, VerificationCodeChannel.Phone), ct);
    }
}