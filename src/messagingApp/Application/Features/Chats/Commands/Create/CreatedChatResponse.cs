using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Commands.Create;
internal class CreatedChatResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ImageIdentifier { get; set; }
    public string? InvitationCode { get; set; }
}

