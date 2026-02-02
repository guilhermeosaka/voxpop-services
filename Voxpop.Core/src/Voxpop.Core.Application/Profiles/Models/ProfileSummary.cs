namespace Voxpop.Core.Application.Profiles.Models;

public record ProfileSummary(
    PersonalInfoSummary PersonalInfo,
    LocationInfoSummary LocationInfo,
    ProfessionalInfoSummary ProfessionalInfo,
    CulturalInfoSummary CulturalInfo
);