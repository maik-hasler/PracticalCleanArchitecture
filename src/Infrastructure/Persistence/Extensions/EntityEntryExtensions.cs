using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence.Extensions;

public static class EntityEntryExtensions
{
    public static bool HasChangedOwnedEntities(
        this EntityEntry entityEntry)
    {
        return entityEntry.References.Any(r =>
            r.TargetEntry != null
            && r.TargetEntry.Metadata.IsOwned()
            && (r.TargetEntry.State == EntityState.Added
                || r.TargetEntry.State == EntityState.Modified));
    }
}