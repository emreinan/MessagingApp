using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.Application.Security;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Rules;

public class AuthBusinessRules(IUserRepository userRepository)
{
    public void UserShouldExist(User? user)
    {
        if (user is null)
            throw new Exception(ErrorMessages.UserNotFound);
    }

    public void PasswordShouldMatch(string password, User user)
    {
        if (!HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            throw new Exception(ErrorMessages.InvalidPassword);
    }

    public async Task EmailShouldBeUnique(string email)
    {
        if (await userRepository.AnyAsync(u => u.Email == email))
            throw new Exception(ErrorMessages.EmailInUse);
    }

    public void RefreshTokenShouldExist(RefreshToken? refreshToken)
    {
        if (refreshToken is null)
            throw new Exception(ErrorMessages.RefreshTokenNotFound);
    }

    public void RefreshTokenShouldBeActive(RefreshToken refreshToken)
    {
        if (refreshToken.Revoked is not null || refreshToken.ExpiresAt <= DateTime.UtcNow)
            throw new Exception(ErrorMessages.InvalidRefreshToken);
    }

    public void IpAddressShouldMatch(RefreshToken refreshToken, string ipAddress)
    {
        if (refreshToken.CreatedByIp != ipAddress)
            throw new Exception(ErrorMessages.IpDoesNotMatch);
    }
}
