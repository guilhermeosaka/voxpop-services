using Voxpop.Core.Domain.UserProfiles.Entities;

namespace Voxpop.Core.Api.Requests;

public record UpsertProfileRequest(
    PersonalInfo? PersonalInfo,
    LocationInfo? LocationInfo,
    ProfessionalInfo? ProfessionalInfo,
    CulturalInfo? CulturalInfo
);