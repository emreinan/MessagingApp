namespace Application.Features.Chats.Queries.GetById;

public class GetByIdChatResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageIdentifier { get; set; }
    public string? InvitationCode { get; set; }
}
