using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Services.Data;

public interface IGroupService
{
    Task<Group?> GetByIdAsync(Guid id);
    Task<IEnumerable<Group>> GetByUserAsync(Guid userId);
    Task<Group?> CreateAsync(string? name, List<Guid> participantIds);
    Task<bool> AddParticipantAsync(Guid groupId, Guid userId);
    Task<bool> IsParticipantAsync(Guid groupId, Guid userId);
}
