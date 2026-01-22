using Voxpop.Identity.Application.Exceptions;
using Voxpop.Identity.Application.Handlers.Verifications.CreatePhoneCode;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Packages.Handler.Interfaces;

namespace Voxpop.Identity.Application.Handlers.User.CreateUser;

public class CreateUserHandler(IUserRepository<Domain.Models.User> userRepository, IHandler handler)
    : IHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken ct)
    {
        var existingUser = await userRepository.FindByPhoneNumberAsync(request.PhoneNumber);

        if (existingUser is { PhoneNumberConfirmed: true })
            throw new UserAlreadyExistsException(request.PhoneNumber);

        if (existingUser == null)
            await userRepository.AddAsync(new Domain.Models.User(request.PhoneNumber));

        await handler.Handle(new CreatePhoneCodeCommand(request.PhoneNumber), ct);
    }
}