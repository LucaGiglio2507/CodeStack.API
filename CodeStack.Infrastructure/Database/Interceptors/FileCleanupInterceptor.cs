using CodeStack.Core.Interfaces.Services.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DomainFile = CodeStack.Domain.Entities.File;

namespace CodeStack.Infrastructure.Database.Interceptors;

public class FileCleanupInterceptor : SaveChangesInterceptor, IFileDeletionTracker
{
    private readonly List<string> _pendingUrls = new();

    public IReadOnlyList<string> PendingUrls => _pendingUrls.AsReadOnly();

    public void Clear() => _pendingUrls.Clear();

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        Collect(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        Collect(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void Collect(DbContext? context)
    {
        if (context is null) return;

        var urls = context.ChangeTracker
            .Entries<DomainFile>()
            .Where(e => e.State == EntityState.Deleted)
            .Select(e => e.Entity.Url);

        _pendingUrls.AddRange(urls);
    }
}
