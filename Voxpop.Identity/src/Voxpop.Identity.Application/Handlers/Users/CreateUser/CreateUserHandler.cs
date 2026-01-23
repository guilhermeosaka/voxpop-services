using Voxpop.Identity.Application.Common;
using Voxpop.Identity.Application.Handlers.Codes.CreateCode;
using Voxpop.Identity.Domain.Enums;
using Voxpop.Identity.Domain.Interfaces;
using Voxpop.Identity.Domain.Models;
using Voxpop.Packages.Handler.Interfaces;
using Voxpop.Packages.Handler.Types;

namespace Voxpop.Identity.Application.Handlers.Users.CreateUser;

public class CreateUserHandler(IUserRepository userRepository, IDispatcher dispatcher)
    : IHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken ct)
    {
        var existingUser = await userRepository.FindByPhoneNumberAsync(request.PhoneNumber);

        if (existingUser != null)
            return Errors.UserConflict(request.PhoneNumber, VerificationCodeChannel.Phone);

        if (existingUser == null)
            await userRepository.AddAsync(User.Create(request.PhoneNumber));

        await dispatcher.Dispatch(new CreateCodeCommand(request.PhoneNumber, VerificationCodeChannel.Phone), ct);
        
        return Result.Success();
    }
}