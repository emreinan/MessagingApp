using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Messages.Rules;

public class MessageBusinessRules
{
    public void ChatShouldExistWhenSelected(Chat? chat)
    {
        if (chat is null)
            throw new BusinessException("Chat not found");
    }
    public void UserShouldExistWhenSelected(User? user)
    {
        if (user is null)
            throw new BusinessException("User not found");
    }
    public void UserShouldBeInChatWhenSelected(Chat chat, User user)
	{
		if (!chat.ChatUsers.Any(cu => cu.UserId == user.Id))
			throw new BusinessException("User is not in chat");
	}
}
