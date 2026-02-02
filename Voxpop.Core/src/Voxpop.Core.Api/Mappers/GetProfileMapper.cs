using Voxpop.Core.Application.Profiles.Models;
using Voxpop.Core.Contracts.Dtos;
using Voxpop.Core.Contracts.Responses;

namespace Voxpop.Core.Api.Mappers;

public static class GetProfileMapper
{
    private static ReferenceInfoDto ToDto(this ReferenceInfoSummary reference) => new(reference.Id, reference.Name);

    private static PersonalInfoDto ToDto(this PersonalInfoSummary info) =>
        new(info.DateOfBirth, info.Gender.ToDto());

    private static LocationInfoDto ToDto(this LocationInfoSummary info) =>
        new(info.City.ToDto(),
            info.State.ToDto(),
            info.Country.ToDto(),
            info.Continent.ToDto());

    private static ProfessionalInfoDto ToDto(this ProfessionalInfoSummary info) =>
        new(info.EducationLevel.ToDto(), info.Occupation.ToDto());

    private static CulturalInfoDto ToDto(this CulturalInfoSummary culturalInfo) =>
        new(culturalInfo.Religion.ToDto(), culturalInfo.Race.ToDto(), culturalInfo.Ethnicity.ToDto());

    public static GetProfileResponse ToResponse(this ProfileSummary profile) =>
        new(profile.PersonalInfo.ToDto(),
            profile.LocationInfo.ToDto(),
            profile.ProfessionalInfo.ToDto(),
            profile.CulturalInfo.ToDto());
}