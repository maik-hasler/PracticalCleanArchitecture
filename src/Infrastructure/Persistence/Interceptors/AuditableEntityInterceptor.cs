using Domain.Common.Abstractions.Auditing;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

internal sealed class AuditableEntityInterceptor(
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

        foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
        {
            var utcNow = _timeProvider.GetUtcNow();

            if (entry.State is EntityState.Added)
            {
                entry.Property(nameof(IAuditableEntity.CreatedOn)).CurrentValue = utcNow;

                continue;
            }

            if (entry.State is EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Property(nameof(IAuditableEntity.ModifiedOn)).CurrentValue = utcNow;
            }
        }
    }
}
