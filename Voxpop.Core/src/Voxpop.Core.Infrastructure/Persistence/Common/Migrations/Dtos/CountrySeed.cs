namespace Voxpop.Core.Infrastructure.Persistence.Common.Migrations.Dtos;

public record CountrySeed(string Code, Dictionary<string, string> Translations, StateSeed[] States)
    : BaseCodeSeed(Code, Translations);