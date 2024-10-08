﻿using WebMVC.Services.DTOs;

namespace WebMVC.Services.Auth;

public class HttpAuthService(IHttpClientFactory httpClientFactory) : IAuthService
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
    public async Task<TokenResponse> LoginAsync(LoginDto loginDto)
    {

        var response = await _httpClient.PostAsJsonAsync("/api/Auth/Login", new { loginDto.Email, loginDto.Password});
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result;
    }

    public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
    {
        var response = await _httpClient.GetAsync($"/api/Auth/RefreshToken?refreshToken={refreshToken}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result;
    }

    public async Task<TokenResponse> RegisterAsync(RegisterDto registerDto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/Auth/Register", new {registerDto.Email, registerDto.Password, registerDto.Nickname});
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
        return result;
    }
}
