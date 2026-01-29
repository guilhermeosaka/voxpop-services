namespace Voxpop.Core.Infrastructure.Persistence.Common.Migrations.Dtos;

public record CitySeed(string Code, Dictionary<string, string> Translations) : BaseCodeSeed(Code, Translations);