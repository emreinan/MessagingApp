using Application.Features.Chats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Chats.Queries.GetById;

public class GetByIdChatQuery : IRequest<GetByIdChatResponse>
{
    public Guid ChatId { get; set; }
    class GetByIdChatQueryHandler(IChatRepository chatRepository, IMapper mapper, ChatBusinessRules chatBusinessRules) : IRequestHandler<GetByIdChatQuery, GetByIdChatResponse>
    {
        public async Task<GetByIdChatResponse> Handle(GetByIdChatQuery request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetAsync(c=>c.Id == request.ChatId);

            chatBusinessRules.ChatShouldExistWhenSelected(chat);

            return mapper.Map<GetByIdChatResponse>(chat);
        }
    }
}
