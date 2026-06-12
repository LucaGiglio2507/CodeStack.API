using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Domain.Entities;
using CodeStack.Domain.Enums;

namespace CodeStack.Core.Services.Data;

/// <summary>
/// CRUD for Kanban boards and member management (add/remove); returns only boards the user created or belongs to.
/// </summary>
public class KanbanService(IKanbanRepository _kanbanRepository) : IKanbanService
{
    public async Task<Kanban?> GetByIdAsync(Guid id)
    {
        return await _kanbanRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Kanban>> GetByUserAsync(Guid userId)
    {
        return await _kanbanRepository.GetByUserAsync(userId);
    }

    public async Task<Kanban?> CreateAsync(Guid creatorId, string name, string? description, string? iconUrl)
    {
        Kanban kanban = new Kanban
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Icon_url = iconUrl,
            Creator_Id = creatorId,
            Created_At = DateTime.UtcNow
        };
        return await _kanbanRepository.CreateAsync(kanban);
    }

    public async Task<bool> UpdateAsync(Guid id, string? name, string? description, string? iconUrl)
    {
        Kanban? kanban = await _kanbanRepository.GetByIdAsync(id);
        if (kanban is null) return false;

        if (name is not null) kanban.Name = name;
        if (description is not null) kanban.Description = description;
        if (iconUrl is not null) kanban.Icon_url = iconUrl;

        return await _kanbanRepository.UpdateAsync(kanban);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _kanbanRepository.DeleteAsync(id);
    }

    public async Task<bool> AddMemberAsync(Guid kanbanId, Guid userId, Roles role)
    {
        bool alreadyMember = await _kanbanRepository.IsMemberAsync(kanbanId, userId);
        if (alreadyMember) return false;

        KanbanMember member = new KanbanMember
        {
            Id = Guid.NewGuid(),
            Kanban_Id = kanbanId,
            User_Id = userId,
            Role = role,
            Joined_At = DateTime.UtcNow
        };
        return await _kanbanRepository.AddMemberAsync(member);
    }

    public async Task<bool> RemoveMemberAsync(Guid kanbanId, Guid userId)
    {
        return await _kanbanRepository.RemoveMemberAsync(kanbanId, userId);
    }
}
