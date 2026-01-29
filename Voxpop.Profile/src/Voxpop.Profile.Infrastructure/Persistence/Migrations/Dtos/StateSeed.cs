namespace Voxpop.Profile.Infrastructure.Persistence.Migrations.Dtos;

public record StateSeed(string Code, Dictionary<string, string> Translations, CitySeed[] Cities)
    : BaseCodeSeed(Code, Translations);