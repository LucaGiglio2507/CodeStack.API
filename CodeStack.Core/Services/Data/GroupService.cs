using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Domain.Entities;

namespace CodeStack.Core.Services.Data;

/// <summary>
/// Creates conversation groups with initial participants, adds new participants, and checks membership before inserting duplicates.
/// </summary>
public class GroupService(IGroupRepository _groupRepository) : IGroupService
{
    public async Task<Group?> GetByIdAsync(Guid id)
    {
        return await _groupRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Group>> GetByUserAsync(Guid userId)
    {
        return await _groupRepository.GetByUserAsync(userId);
    }

    public async Task<Group?> CreateAsync(string? name, List<Guid> participantIds)
    {
        Group group = new Group
        {
            Id = Guid.NewGuid(),
            Name = name,
            CreatedAt = DateTime.UtcNow
        };

        Group? created = await _groupRepository.CreateAsync(group);
        if (created is null) return null;

        foreach (Guid userId in participantIds)
            await _groupRepository.AddParticipantAsync(created.Id, userId);

        return await _groupRepository.GetByIdAsync(created.Id);
    }

    public async Task<bool> AddParticipantAsync(Guid groupId, Guid userId)
    {
        bool alreadyIn = await _groupRepository.IsParticipantAsync(groupId, userId);
        if (alreadyIn) return false;
        return await _groupRepository.AddParticipantAsync(groupId, userId);
    }

    public async Task<bool> IsParticipantAsync(Guid groupId, Guid userId)
    {
        return await _groupRepository.IsParticipantAsync(groupId, userId);
    }
}
