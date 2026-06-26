using CodeStack.API.Dtos.Responses.Synthesis;
using CodeStack.Domain.Entities;

namespace CodeStack.API.Mappers;

public static class SynthesisMapper
{
    public static SynthesisResponseDto ToDto(Synthesis synthesis) => new()
    {
        Id = synthesis.Id,
        Title = synthesis.Title,
        Description = synthesis.Description,
        Content = synthesis.Content,
        IsSnippet = synthesis.IsSnippet,
        Archived = synthesis.Archived,
        CreatedAt = synthesis.Created_At,
        UserId = synthesis.User_Id,
        FolderId = synthesis.FolderId
    };

    public static IEnumerable<SynthesisResponseDto> ToDtoList(IEnumerable<Synthesis> syntheses)
        => syntheses.Select(ToDto);
}
