using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Voxpop.Profile.Domain.Interfaces;
using Voxpop.Profile.Domain.UserProfiles;

namespace Voxpop.Profile.Infrastructure.Persistence.Interceptors;

public class AuditSaveChangesInterceptor(IClock clock) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateAuditFields(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditFields(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditFields(DbContext? context)
    {
        if (context is null) return;

        context.ChangeTracker.DetectChanges();

        var now = clock.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<UserProfile>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.ModifiedAt = now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedAt = now;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsArchived = true;
                    entry.Entity.ArchivedAt = now;
                    break;
            }
        }
    }
}