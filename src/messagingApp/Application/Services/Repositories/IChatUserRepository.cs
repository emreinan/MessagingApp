using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IChatUserRepository : IRepository<ChatUser, Guid>, IAsyncRepository<ChatUser, Guid>
{
}


