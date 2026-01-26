namespace Voxpop.Profile.Domain.Models.Abstractions;

public class BaseCodeModel(Guid id, string code) : BaseModel(id)
{
    public string Code { get; private set; } = code;
}