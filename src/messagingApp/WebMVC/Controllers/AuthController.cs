using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services.Auth;
using WebMVC.Services.DTOs;
using WebMVC.Services.Token;

namespace WebMVC.Controllers;

public class AuthController(IAuthService authService,
    IMapper mapper,
    ITokenService tokenService
    ) : Controller
{
    [HttpGet("/Login")]
	public async Task<IActionResult> Login()
	{
		return View();
	}

	[HttpPost("/Login")]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
		if (!ModelState.IsValid)
			return View(loginViewModel);

        var loginDto = mapper.Map<LoginDto>(loginViewModel);
		var token = await authService.LoginAsync(loginDto);

        tokenService.SetRefreshToken(token.RefreshToken);
		tokenService.SetAccessToken(token.AccessToken);

		TempData["SuccessMessage"] = "Login successfully!";

		return RedirectToAction("Index", "Home");
    }

    [HttpGet("/Register")]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost("/Register")]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
            return View(registerViewModel);

        var registerDto = mapper.Map<RegisterDto>(registerViewModel);

        var token = await authService.RegisterAsync(registerDto);
        tokenService.SetRefreshToken(token.RefreshToken);
        tokenService.SetAccessToken(token.AccessToken);

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Verify()
    {
        return View();
    }


}
