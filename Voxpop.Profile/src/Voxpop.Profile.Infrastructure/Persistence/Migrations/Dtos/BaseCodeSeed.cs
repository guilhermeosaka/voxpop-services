namespace Voxpop.Profile.Infrastructure.Persistence.Migrations.Dtos;

public record BaseCodeSeed(string Code, Dictionary<string, string> Translations);