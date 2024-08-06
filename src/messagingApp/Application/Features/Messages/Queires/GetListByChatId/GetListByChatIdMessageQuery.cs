using Application.Features.Messages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Queires.GetListByChatId;

public class GetListByChatIdMessageQuery : IRequest<List<GetListByChatIdMessageListItemDto>>
{
    public Guid ChatId { get; set; }

    class GetListByChatIdMessageQueryHandler(IMessageRepository messageRepository, 
        IMapper mapper,
        IChatRepository chatRepository,
        MessageBusinessRules messageBusinessRules
        ) : IRequestHandler<GetListByChatIdMessageQuery, List<GetListByChatIdMessageListItemDto>>
    {


        public async Task<List<GetListByChatIdMessageListItemDto>> Handle(GetListByChatIdMessageQuery request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetAsync(c=>c.Id==request.ChatId);
            messageBusinessRules.ChatShouldExistWhenSelected(chat);

            var messages = await messageRepository.GetListAsync(
                predicate: m => m.ChatId == request.ChatId,
                include: m => m.Include(m => m.User),
                orderBy: m => m.OrderBy(m => m.SentAt)
                );

            return mapper.Map<List<GetListByChatIdMessageListItemDto>>(messages);
        }
    }
}

