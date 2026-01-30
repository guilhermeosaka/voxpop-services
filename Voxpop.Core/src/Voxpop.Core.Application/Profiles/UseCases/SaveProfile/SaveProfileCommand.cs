using Voxpop.Core.Domain.Profiles.Entities;

namespace Voxpop.Core.Application.Profiles.UseCases.SaveProfile;

public record SaveProfileCommand(
    PersonalInfo? PersonalInfo,
    LocationInfo? LocationInfo,
    ProfessionalInfo? ProfessionalInfo,
    CulturalInfo? CulturalInfo
);