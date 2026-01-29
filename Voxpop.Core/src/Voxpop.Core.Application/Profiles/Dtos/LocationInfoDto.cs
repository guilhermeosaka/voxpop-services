using Voxpop.Core.Application.ReferenceData.Dtos;

namespace Voxpop.Core.Application.Profiles.Dtos;

public record LocationInfoDto(ReferenceDto? City, ReferenceDto? State, ReferenceDto? Country, ReferenceDto? Continent);