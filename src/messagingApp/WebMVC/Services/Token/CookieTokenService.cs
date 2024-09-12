﻿namespace WebMVC.Services.Token;

public class CookieTokenService(IHttpContextAccessor httpContextAccessor) : ITokenService
{
	public string? GetAccessToken()
	{
		return httpContextAccessor.HttpContext.Request.Cookies["access_token"];
	}

	public string? GetRefreshToken()
	{
		return httpContextAccessor.HttpContext.Request.Cookies["refresh_token"];
	}

	public void SetAccessToken(string accessToken)
	{
		httpContextAccessor.HttpContext.Response.Cookies.Append("access_token", accessToken, new CookieOptions
		{
			Secure = true,
			Expires = DateTimeOffset.UtcNow.AddMinutes(10)
		});
	}

	public void SetRefreshToken(string refreshToken)
	{
		httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
		{
			Secure = true,
			Expires = DateTimeOffset.UtcNow.AddMinutes(10)
		});
	}
}
