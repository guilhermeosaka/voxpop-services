using Voxpop.Core.Application.ReferenceData.Dtos;

namespace Voxpop.Core.Application.UserProfiles.Dtos;

public record LocationInfoDto(ReferenceDto? City, ReferenceDto? State, ReferenceDto? Country, ReferenceDto? Continent);