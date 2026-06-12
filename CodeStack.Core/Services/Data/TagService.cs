using CodeStack.Core.Interfaces.Repositories;
using CodeStack.Core.Interfaces.Services.Data;
using CodeStack.Domain.Entities;

namespace CodeStack.Core.Services.Data;

/// <summary>
/// CRUD for user-owned labels (name + colour) used to categorise tasks.
/// </summary>
public class TagService(ITagRepository _tagRepository) : ITagService
{
    public async Task<Tag?> GetByIdAsync(Guid id)
    {
        return await _tagRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Tag>> GetByUserAsync(Guid userId)
    {
        return await _tagRepository.GetByUserAsync(userId);
    }

    public async Task<Tag?> CreateAsync(Guid userId, string name, string color)
    {
        Tag tag = new Tag
        {
            Id = Guid.NewGuid(),
            Name = name,
            Color = color,
            User_Id = userId
        };
        return await _tagRepository.CreateAsync(tag);
    }

    public async Task<bool> UpdateAsync(Guid id, string? name, string? color)
    {
        Tag? tag = await _tagRepository.GetByIdAsync(id);
        if (tag is null) return false;

        if (name is not null) tag.Name = name;
        if (color is not null) tag.Color = color;

        return await _tagRepository.UpdateAsync(tag);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _tagRepository.DeleteAsync(id);
    }
}
