namespace Voxpop.Core.Application.Dtos;

public record LocationInfoDto(ReferenceDto? City, ReferenceDto? State, ReferenceDto? Country, ReferenceDto? Continent);