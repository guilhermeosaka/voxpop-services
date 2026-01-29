namespace Voxpop.Core.Infrastructure.Persistence.Common.Migrations.Dtos;

public record StateSeed(string Code, Dictionary<string, string> Translations, CitySeed[] Cities)
    : BaseCodeSeed(Code, Translations);