using CodeStack.API.Dtos.Responses.Message;
using CodeStack.Domain.Entities;

namespace CodeStack.API.Mappers;

public static class MessageMapper
{
    public static MessageResponseDto ToDto(Message message) => new()
    {
        Id = message.Id,
        Content = message.Content,
        CreatedAt = message.CreatedAt,
        ReadAt = message.ReadAt,
        GroupId = message.Group_Id,
        Sender = new MessageSenderDto
        {
            Id = message.Sender.Id,
            Name = $"{message.Sender.First_name} {message.Sender.Name}",
            AvatarUrl = message.Sender.Avatar_Url
        }
    };

    public static IEnumerable<MessageResponseDto> ToDtoList(IEnumerable<Message> messages)
        => messages.Select(ToDto);
}
