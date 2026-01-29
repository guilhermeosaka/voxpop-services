namespace Voxpop.Core.Infrastructure.Persistence.Common.Migrations.Dtos;

public record ContinentSeed(string Code, Dictionary<string, string> Translations, CountrySeed[] Countries)
    : BaseCodeSeed(Code, Translations);