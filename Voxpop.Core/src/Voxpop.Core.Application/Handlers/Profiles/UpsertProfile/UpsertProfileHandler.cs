using Voxpop.Core.Application.Interfaces;
using Voxpop.Core.Domain.Interfaces;
using Voxpop.Core.Domain.UserProfiles;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Handlers.Profiles.UpsertProfile;

public class UpsertProfileHandler(
    IUserProfileRepository userProfileRepository, 
    IUnitOfWork unitOfWork, 
    IRequestContext requestContext)
    : IHandler<UpsertProfileCommand>
{
    public async Task<Result> Handle(UpsertProfileCommand request, CancellationToken ct)
    {
        var userId = requestContext.UserId;
        
        var userProfile = await userProfileRepository.FindByUserIdAsync(userId);

        if (userProfile == null)
        {
            userProfile = UserProfile.Create(userId);
            await userProfileRepository.AddAsync(userProfile);
        }
        
        if (request.PersonalInfo != null) userProfile.UpdatePersonalInfo(request.PersonalInfo);
        if (request.LocationInfo != null) userProfile.UpdateLocation(request.LocationInfo);
        if (request.ProfessionalInfo != null) userProfile.UpdateProfessionalInfo(request.ProfessionalInfo);
        if (request.CulturalInfo != null) userProfile.UpdateCulturalInfo(request.CulturalInfo);

        await unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}