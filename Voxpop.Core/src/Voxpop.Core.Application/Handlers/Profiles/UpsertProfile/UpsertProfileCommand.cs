using Voxpop.Core.Domain.UserProfiles;

namespace Voxpop.Core.Application.Handlers.Profiles.UpsertProfile;

public record UpsertProfileCommand(
    PersonalInfo? PersonalInfo,
    LocationInfo? LocationInfo,
    ProfessionalInfo? ProfessionalInfo,
    CulturalInfo? CulturalInfo
);