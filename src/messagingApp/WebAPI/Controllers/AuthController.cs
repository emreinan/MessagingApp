using Application.Features.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserForLoginDto dto)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            LoginCommand command = new LoginCommand { Login = dto, IpAddress = ipAddress };
            var response = await mediator.Send(command);
            return Ok(response);
        }
    }
}
