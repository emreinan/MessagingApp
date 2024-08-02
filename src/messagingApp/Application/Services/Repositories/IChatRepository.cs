using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IChatRepository : IRepository<Chat, Guid>, IAsyncRepository<Chat, Guid>
{
}


