
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebMVC.Services.Auth;
using WebMVC.Services.Token;

namespace WebMVC;

public static class MVCServiceRegistrations

{
	public static IServiceCollection AddMvcServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient("ApiClient", client =>
		{
			string apiUrl = configuration["ApiUrl"] ?? throw new InvalidOperationException();
			client.BaseAddress = new Uri(apiUrl);
		});

		services.AddHttpContextAccessor();
		services.AddScoped<IAuthService, HttpAuthService>();
		services.AddScoped<ITokenService, CookieTokenService>();

		TokenOptions tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>()
			?? throw new InvalidOperationException("TokenOptions cant found in configuration");

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
		   .AddJwtBearer(options =>
		   {
			   options.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidateIssuer = true,
				   ValidateAudience = true,
				   ValidateLifetime = true,
				   ValidIssuer = tokenOptions.Issuer,
				   ValidAudience = tokenOptions.Audience,
				   ValidateIssuerSigningKey = true,
				   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))
			   };

			   options.Events = new JwtBearerEvents
			   {
				   OnMessageReceived = context =>
				   {
					   var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
					   context.Token = tokenService.GetAccessToken();

					   return Task.CompletedTask;
				   },

				   OnChallenge = async context => // Access token is expired or invalid
				   {
					   var tokenService = context.HttpContext.RequestServices.GetRequiredService<ITokenService>();
					   var refreshToken = tokenService.GetRefreshToken();

					   if (string.IsNullOrEmpty(refreshToken)) // Refresh token is not found
					   {
						   context.HandleResponse();
						   context.Response.Redirect("/Login");
					   }

					   try
					   {
						   var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
						   var tokenResponse = await authService.RefreshTokenAsync(refreshToken);
						   tokenService.SetAccessToken(tokenResponse.AccessToken);
						   tokenService.SetRefreshToken(tokenResponse.RefreshToken);
						   context.HandleResponse();
						   context.Response.Redirect("/");
					   }
					   catch
					   {
						   context.HandleResponse();
						   context.Response.Redirect("/Login");
					   }
				   }
			   };
		   });

		return services;
	}
}
