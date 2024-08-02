using Core.Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MessageUserStateRepository : EfRepositoryBase<MessageUserState, Guid, AppDbContext>, IMessageUserStateRepository
{
    public MessageUserStateRepository(AppDbContext context) : base(context)
    {
    }
}
