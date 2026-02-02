namespace Voxpop.Core.Contracts.Dtos;

public record LocationInfoDto(ReferenceInfoDto? City, ReferenceInfoDto? State, ReferenceInfoDto? Country, ReferenceInfoDto? Continent);