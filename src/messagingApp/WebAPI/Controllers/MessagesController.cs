using Application.Features.Messages.Commands.SendMessage;
using Application.Features.Messages.Queires.GetListByChatId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessagesController(IMediator mediator) : ControllerBase
{
    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("GetByChatId/{ChatId}")]
    public async Task<IActionResult> GetMessageByChatId([FromRoute] Guid ChatId)
    {
        var response = await mediator.Send(new GetListByChatIdMessageQuery { ChatId = ChatId });
        return Ok(response);
    }
}
