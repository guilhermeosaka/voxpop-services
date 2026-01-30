using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Domain.Profiles.Entities;
using Voxpop.Core.Domain.Profiles.Repositories;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Profiles.UseCases.SaveProfile;

public class SaveProfileHandler(
    IProfileRepository repository, 
    IUnitOfWork unitOfWork, 
    IRequestContext requestContext)
    : IHandler<SaveProfileCommand>
{
    public async Task<Result> Handle(SaveProfileCommand request, CancellationToken ct)
    {
        var userId = requestContext.UserId;
        
        var profile = await repository.FindByUserIdAsync(userId);

        if (profile == null)
        {
            profile = Profile.Create(userId);
            await repository.AddAsync(profile);
        }
        
        if (request.PersonalInfo != null) profile.UpdatePersonalInfo(request.PersonalInfo);
        if (request.LocationInfo != null) profile.UpdateLocation(request.LocationInfo);
        if (request.ProfessionalInfo != null) profile.UpdateProfessionalInfo(request.ProfessionalInfo);
        if (request.CulturalInfo != null) profile.UpdateCulturalInfo(request.CulturalInfo);

        await unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}