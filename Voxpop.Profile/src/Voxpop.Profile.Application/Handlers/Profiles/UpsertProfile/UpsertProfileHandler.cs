using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;
using Voxpop.Profile.Application.Interfaces;
using Voxpop.Profile.Domain.Interfaces;
using Voxpop.Profile.Domain.UserProfiles;

namespace Voxpop.Profile.Application.Handlers.Profiles.UpsertProfile;

public class UpsertProfileHandler(
    IUserProfileRepository userProfileRepository, 
    IUnitOfWork unitOfWork, 
    IRequestContext requestContext)
    : IHandler<UpsertProfileCommand>
{
    public async Task<Result> Handle(UpsertProfileCommand request, CancellationToken ct)
    {
        if (!requestContext.UserId.HasValue)
            throw new UnauthorizedAccessException();
        
        var userId = requestContext.UserId.Value;
        
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