using Voxpop.Profile.Domain.UserProfiles;

namespace Voxpop.Profile.Application.Handlers.Profiles.UpsertProfile;

public record UpsertProfileCommand(
    PersonalInfo? PersonalInfo,
    LocationInfo? LocationInfo,
    ProfessionalInfo? ProfessionalInfo,
    CulturalInfo? CulturalInfo
);