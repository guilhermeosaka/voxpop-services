namespace Voxpop.Core.Infrastructure.Persistence.Migrations.Dtos;

public record BaseCodeSeed(string Code, Dictionary<string, string> Translations);