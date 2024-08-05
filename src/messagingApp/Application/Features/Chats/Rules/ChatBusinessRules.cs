using Application.Features.Auth.Constants;
using Application.Features.Chats.Constant;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Chats.Rules;

public class ChatBusinessRules(IChatRepository chatRepository)
{
    public void ChatShouldExistWhenSelected(Chat? chat)
    {
        if (chat is null)
            throw new Exception("Chat not found");
    }
    public void UserShouldExistWhenSelected(User? user)
    {
        if (user is null)
            throw new Exception("Chat not found");
    }

    public void UserShouldNotBeInChatWhenJoining(ChatUser? chatUser)
    {
        if (chatUser is not null)
            throw new Exception("User is already in this chat");
    }

    public void CheckUserLimitWhenJoining(Chat chat)
    {
        if (chat.ChatUsers.Count>= ChatConstants.MaxUsersInChat)
            throw new Exception("Chat is full");
    }
}

