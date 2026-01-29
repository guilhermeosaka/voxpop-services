using Voxpop.Core.Application.ReferenceData.Dtos;

namespace Voxpop.Core.Application.Profiles.Dtos;

public record PersonalInfoDto(DateOnly? DateOfBirth, ReferenceDto? Gender);