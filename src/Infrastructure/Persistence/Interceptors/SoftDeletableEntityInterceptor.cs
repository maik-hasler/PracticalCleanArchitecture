using Domain.Common.Abstractions.Auditing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

internal sealed class SoftDeletableEntityInterceptor(
    TimeProvider timeProvider)
    : SaveChangesInterceptor
{
    private readonly TimeProvider _timeProvider = timeProvider;

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<ISoftDeletableEntity>())
        {
            if (entry.State is EntityState.Deleted)
            {
                entry.Property(nameof(ISoftDeletableEntity.Deleted)).CurrentValue = true;

                entry.Property(nameof(ISoftDeletableEntity.DeletedOn)).CurrentValue = _timeProvider.GetUtcNow();

                entry.State = EntityState.Modified;

                UpdateDeletedEntityEntryReferencesToUnchanged(entry);
            }
        }
    }

    private static void UpdateDeletedEntityEntryReferencesToUnchanged(EntityEntry entityEntry)
    {
        if (!entityEntry.References.Any())
        {
            return;
        }

        foreach (ReferenceEntry referenceEntry in entityEntry.References.Where(r => r.TargetEntry!.State == EntityState.Deleted))
        {
            referenceEntry.TargetEntry!.State = EntityState.Unchanged;

            UpdateDeletedEntityEntryReferencesToUnchanged(referenceEntry.TargetEntry);
        }
    }
}