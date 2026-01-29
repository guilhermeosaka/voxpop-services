using Voxpop.Core.Application.ReferenceData.Dtos;

namespace Voxpop.Core.Application.UserProfiles.Dtos;

public record PersonalInfoDto(DateOnly? DateOfBirth, ReferenceDto? Gender);