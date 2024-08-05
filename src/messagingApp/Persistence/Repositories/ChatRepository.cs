﻿using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ChatRepository : EfRepositoryBase<Chat, Guid, AppDbContext>, IChatRepository
{
    public ChatRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<Chat>> GetChatsByUserIdAsync(Guid UserId)
    {
        var chats = await Context.Chats
            .Include(c => c.ChatUsers)
            .Include(c => c.Messages)
            .ThenInclude(cu => cu.User)
            .Where(c => c.ChatUsers.Any(cu => cu.UserId == UserId))
            .OrderByDescending(c => c.Messages.OrderByDescending(c=>c.SentAt).FirstOrDefault().SentAt)
            .ToListAsync();
        
        return chats;
    }
}

