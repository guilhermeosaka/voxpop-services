using Voxpop.Core.Domain.UserProfiles;

namespace Voxpop.Core.Api.Requests;

public record UpsertProfileRequest(
    PersonalInfo? PersonalInfo,
    LocationInfo? LocationInfo,
    ProfessionalInfo? ProfessionalInfo,
    CulturalInfo? CulturalInfo
);