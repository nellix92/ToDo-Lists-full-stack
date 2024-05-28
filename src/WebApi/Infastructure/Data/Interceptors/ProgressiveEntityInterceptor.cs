using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using webapi.Domains.Entities;
using webapi.Services;

namespace webapi.Infastructure.Data.Interceptors;

public class ProgressiveEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<IProgressive>())
        {
            if(entry.Entity.GetType().Equals(typeof(ToDoList)))
            {
                foreach (var collection in entry.Collections)
                {
                    if (collection.CurrentValue is List<ToDoItem> toDoItems)
                    {
                        if (toDoItems.Any(item => entry.Context.Entry(item).State == EntityState.Added))
                        {
                            entry.Entity.Progressive++;
                        }
                    }
                }
            }
        }
    }
}
