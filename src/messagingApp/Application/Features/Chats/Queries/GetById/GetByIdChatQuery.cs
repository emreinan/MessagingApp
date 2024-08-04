using Application.Features.Chats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Chats.Queries.GetById;

public class GetByIdChatQuery : IRequest<GetByIdChatResponse>
{
    public Guid ChatId { get; set; }
    class GetByIdChatQueryHandler(IChatRepository chatRepository, IMapper mapper, ChatBusinessRules chatBusinessRules) : IRequestHandler<GetByIdChatQuery, GetByIdChatResponse>
    {
        public async Task<GetByIdChatResponse> Handle(GetByIdChatQuery request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetAsync(
                predicate: c=>c.Id == request.ChatId,
                include: i=>i.Include(c=>c.ChatUsers).ThenInclude(cu=>cu.User));
            chatBusinessRules.ChatShouldExistWhenSelected(chat);

            var chatUser = chat.ChatUsers.Select(cu =>
                new ChatUserDto
                {
                    UserId = cu.User.Id,
                    Nickname = cu.User.Nickname
                }).ToList();

            var response = mapper.Map<GetByIdChatResponse>(chat);
            response.ChatUsers = chatUser;

            return response;
        }
    }
}
