using Voxpop.Core.Contracts.Responses;

namespace Voxpop.Core.Api.Mappers;

public static class CreatePollMapper
{
    public static CreatePollResponse ToResponse(this Guid id) => new(id);
}