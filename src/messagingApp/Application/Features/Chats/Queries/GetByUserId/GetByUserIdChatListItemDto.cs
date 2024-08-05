﻿namespace Application.Features.Chats.Queries.GetByUserId;

public class GetByUserIdChatListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ImageIdentifier { get; set; }
    public string? InvitationCode { get; set; }
    public string? LastMessage { get; set; } = default!;
    public DateTime? LastMessageDate { get; set; }
}