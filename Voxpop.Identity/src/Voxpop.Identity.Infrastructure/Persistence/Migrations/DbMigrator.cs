using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Voxpop.Identity.Infrastructure.Persistence.Migrations;

public class DbMigrator(IServiceScopeFactory scopeFactory, ILogger<DbMigrator> logger)
{
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public async Task MigrateAsync(CancellationToken ct = default)
    {
        await ApplyMigrationsAsync(ct);
    }

    private async Task ApplyMigrationsAsync(CancellationToken ct)
    {
        logger.LogInformation("Applying migrations...");
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        await dbContext.Database.MigrateAsync(ct);
        logger.LogInformation("Migrations applied.");
    }
}