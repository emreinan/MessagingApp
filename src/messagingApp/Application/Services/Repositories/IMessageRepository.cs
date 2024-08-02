using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IMessageRepository : IRepository<Message, Guid>, IAsyncRepository<Message, Guid>
{
}


