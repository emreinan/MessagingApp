using Application.Features.Chats.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Queries.GetByUserId;

public class GetByUserIdChatQuery : IRequest<List<GetByUserIdChatListItemDto>>
{
    public Guid UserId { get; set; }

    class GetByUserIdChatQueryHandler(
        IChatRepository chatRepository,
        IUserRepository userRepository,
        ChatBusinessRules chatBusinessRules,
        IMapper mapper
        ) : IRequestHandler<GetByUserIdChatQuery, List<GetByUserIdChatListItemDto>>
    {

        public async Task<List<GetByUserIdChatListItemDto>> Handle(GetByUserIdChatQuery request, CancellationToken cancellationToken)
        {
            var chats = await chatRepository.GetChatsByUserIdAsync(request.UserId);
           
            var chatListItems = mapper.Map<List<GetByUserIdChatListItemDto>>(chats);

            return chatListItems;
        }
    }
}
