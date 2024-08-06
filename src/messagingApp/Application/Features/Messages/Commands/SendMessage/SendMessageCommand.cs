﻿using Application.Features.Messages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Commands.SendMessage;

public class SendMessageCommand : IRequest<SendMessageResponse>
{
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public string? Content { get; set; }
    public string? FileIdentifier { get; set; }

    class SendMessageCommandHandler(
        IUserRepository userRepository,
        IChatRepository chatRepository,
        IMessageRepository messageRepository,
        IMessageUserStateRepository messageUserStateRepository,
        MessageBusinessRules messageBusinessRules,
        IMapper mapper
        ) : IRequestHandler<SendMessageCommand, SendMessageResponse>
    {


        public async Task<SendMessageResponse> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var chat = await chatRepository.GetAsync(
               predicate: c=>c.Id==request.ChatId,
               include: i=>i.Include(u=>u.ChatUsers));
            messageBusinessRules.ChatShouldExistWhenSelected(chat);

            var senderUser = await userRepository.GetAsync(u => u.Id == request.UserId);
            messageBusinessRules.UserShouldExistWhenSelected(senderUser);
            
            var message = mapper.Map<Message>(request);
            message.SentAt = DateTime.UtcNow;
            await messageRepository.AddAsync(message);

            var messageUserStates = chat!.ChatUsers.Select(c => new MessageUserState
            {
                UserId = c.UserId,
                MessageId = message.Id,
                //IsRead = cu.UserId == request.UserId
                DeliveredAt = senderUser!.Id == c.UserId ? DateTime.UtcNow : null, //Kendi attığım mesajı okundu sayıyorum. kaldırabiliriz.
                ReadAt = senderUser.Id == c.UserId ? DateTime.UtcNow : null
            }).ToList();

            await messageUserStateRepository.AddRangeAsync(messageUserStates);

            var response = mapper.Map<SendMessageResponse>(message);
            return response;
        }
    }
}