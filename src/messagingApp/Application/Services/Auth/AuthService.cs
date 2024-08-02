﻿using Application.Services.Repositories;
using Core.Application.Jwt;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Auth;

public class AuthService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository) : IAuthService
{
    public string CreateAccessToken(User user)
    {

        TokenOptions tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>()
            ?? throw new InvalidOperationException("TokenOptions cant found in configuration");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Nickname),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(tokenOptions.AccessTokenExpiration);

        var jwt = new JwtSecurityToken(
            tokenOptions.Issuer,
            tokenOptions.Audience,
            expires: expires,
            notBefore: DateTime.UtcNow,
            claims: claims,
            signingCredentials: signingCredentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var accessToken = tokenHandler.WriteToken(jwt);
        return accessToken;
    }

    public async Task<RefreshToken> CreateRefreshTokenAsync(User user, string ipAddress)
    {
        string refreshToken = Guid.NewGuid().ToString();
        var newRefreshToken = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.Now.AddMinutes(Convert.ToDouble(configuration["TokenOptions:RefreshTokenExpiration"])),
            CreatedByIp = ipAddress
        };

        await refreshTokenRepository.AddAsync(newRefreshToken);
        return newRefreshToken;
    }

    public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
    {
        refreshToken.Revoked = DateTime.UtcNow;
        refreshToken.ReasonRevoked = "Replaced by new token";
        var newRefreshToken = await CreateRefreshTokenAsync(user, ipAddress);
        refreshToken.ReplacedByToken = newRefreshToken.Token;
        await refreshTokenRepository.UpdateAsync(refreshToken);
        return newRefreshToken;
    }

    public async Task DeleteOldRefreshTokens(Guid userId)
    {
        var oldTokens = await refreshTokenRepository.GetListAsync(t => t.UserId == userId && t.Revoked == null);
        await refreshTokenRepository.DeleteRangeAsync(oldTokens);
    }
}
