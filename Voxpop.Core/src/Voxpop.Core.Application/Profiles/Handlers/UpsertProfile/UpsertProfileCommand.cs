using Voxpop.Core.Domain.UserProfiles.Entities;

namespace Voxpop.Core.Application.Profiles.Handlers.UpsertProfile;

public record UpsertProfileCommand(
    PersonalInfo? PersonalInfo,
    LocationInfo? LocationInfo,
    ProfessionalInfo? ProfessionalInfo,
    CulturalInfo? CulturalInfo
);