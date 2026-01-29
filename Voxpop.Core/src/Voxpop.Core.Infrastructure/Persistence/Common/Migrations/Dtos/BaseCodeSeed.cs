namespace Voxpop.Core.Infrastructure.Persistence.Common.Migrations.Dtos;

public record BaseCodeSeed(string Code, Dictionary<string, string> Translations);