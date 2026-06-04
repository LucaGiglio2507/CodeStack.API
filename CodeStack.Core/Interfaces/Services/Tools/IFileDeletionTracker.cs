namespace CodeStack.Core.Interfaces.Services.Tools;

public interface IFileDeletionTracker
{
    IReadOnlyList<string> PendingUrls { get; }
    void Clear();
}
