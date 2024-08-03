﻿using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Refresh;
using Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserForLoginDto dto)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            LoginCommand command = new() { Login = dto, IpAddress = ipAddress };
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserForRegisterDto dto)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            RegisterCommand command = new() { Register = dto, IpAddress = ipAddress };
            var response = await mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefershTokenAsync([FromQuery] string refreshtToken)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            RefreshTokenCommand command = new() { Token = refreshtToken, IpAddress = ipAddress };
            var response = await mediator.Send(command);
            return Ok(response);
        }
    }
}
