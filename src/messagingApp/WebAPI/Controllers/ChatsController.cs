using Application.Features.Chats.Commands.Create;
using Application.Features.Chats.Commands.Join;
using Application.Features.Chats.Queries.GetById;
using Application.Features.Chats.Queries.GetByUserId;
using Application.Features.Chats.Queries.GetList;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatsController(IMediator mediator) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await mediator.Send(new GetListChatQuery());
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var response = await mediator.Send(new GetByIdChatQuery() { ChatId = id});
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateChatCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("Join")]
    public async Task<IActionResult> Join([FromBody] JoinChatCommand command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpGet("GetByUserId/{UserId}")]
    public async Task<IActionResult> GetByUserId([FromRoute] Guid UserId)
    {
        var response = await mediator.Send(new GetByUserIdChatQuery { UserId = UserId});
        return Ok(response);
    }
}
