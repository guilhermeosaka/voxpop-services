namespace Voxpop.Profile.Infrastructure.Persistence.Migrations.Dtos;

public record CountrySeed(string Code, Dictionary<string, string> Translations, StateSeed[] States)
    : BaseCodeSeed(Code, Translations);