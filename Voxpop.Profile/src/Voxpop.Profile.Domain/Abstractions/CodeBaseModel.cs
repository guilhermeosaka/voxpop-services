namespace Voxpop.Profile.Domain.Abstractions;

public class CodeBaseModel(Guid id, string code) : BaseModel(id)
{
    public string Code { get; private set; } = code;
}