using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IRefreshTokenRepository : IRepository<RefreshToken, Guid>, IAsyncRepository<RefreshToken, Guid>
{
}


