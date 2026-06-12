using CodeStack.Domain.Entities;

namespace CodeStack.Core.Interfaces.Repositories;

public interface IGroupRepository
{
    Task<Group?> GetByIdAsync(Guid id);
    Task<IEnumerable<Group>> GetByUserAsync(Guid userId);
    Task<Group?> CreateAsync(Group group);
    Task<bool> AddParticipantAsync(Guid groupId, Guid userId);
    Task<bool> IsParticipantAsync(Guid groupId, Guid userId);
}
