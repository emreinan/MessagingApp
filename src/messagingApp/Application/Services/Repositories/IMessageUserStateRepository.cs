using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IMessageUserStateRepository : IRepository<MessageUserState, Guid>, IAsyncRepository<MessageUserState, Guid>
{
}


