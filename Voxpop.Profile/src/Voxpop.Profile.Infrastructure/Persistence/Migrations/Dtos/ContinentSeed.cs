namespace Voxpop.Profile.Infrastructure.Persistence.Migrations.Dtos;

public record ContinentSeed(string Code, Dictionary<string, string> Translations, CountrySeed[] Countries)
    : BaseCodeSeed(Code, Translations);