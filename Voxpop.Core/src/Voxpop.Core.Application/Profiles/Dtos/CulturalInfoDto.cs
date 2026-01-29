using Voxpop.Core.Application.ReferenceData.Dtos;

namespace Voxpop.Core.Application.Profiles.Dtos;

public record CulturalInfoDto(ReferenceDto? Religion, ReferenceDto? Race, ReferenceDto Ethnicity);