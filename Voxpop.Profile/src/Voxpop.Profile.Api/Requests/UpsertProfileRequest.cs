using Voxpop.Profile.Domain.UserProfiles;

namespace Voxpop.Profile.Api.Requests;

public record UpsertProfileRequest(
    PersonalInfo? PersonalInfo,
    LocationInfo? LocationInfo,
    ProfessionalInfo? ProfessionalInfo,
    CulturalInfo? CulturalInfo
);