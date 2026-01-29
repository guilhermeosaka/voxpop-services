namespace Voxpop.Core.Infrastructure.Persistence.Migrations.Dtos;

public record CitySeed(string Code, Dictionary<string, string> Translations) : BaseCodeSeed(Code, Translations);