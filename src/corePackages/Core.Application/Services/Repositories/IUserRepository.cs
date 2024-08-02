using Core.Persistence.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services.Repositories;

public interface IUserRepository : IRepository<User, Guid> , IAsyncRepository<User, Guid>
{
}

public interface IMessageRepository : IRepository<Message, Guid> , IAsyncRepository<Message, Guid>
{
}

public interface IMessageUserStateRepository : IRepository<MessageUserState, Guid> , IAsyncRepository<MessageUserState, Guid>
{
}

public interface IRefreshTokenRepository : IRepository<RefreshToken, Guid> , IAsyncRepository<RefreshToken, Guid>
{
}

public interface IChatRepository : IRepository<Chat, Guid> , IAsyncRepository<Chat, Guid>
{
}

public interface IChatUserRepository : IRepository<ChatUser, Guid> , IAsyncRepository<ChatUser, Guid>
{
}


