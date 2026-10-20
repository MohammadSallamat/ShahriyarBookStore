using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Users.DTOs;
using Query.Users.GetByFilter;
using Query.Users.GetById;
using Query.Users.GetByPhoneNumber;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] UserFilterParams filterParams)
    {
        var result = await _mediator.Send(new GetUsersByFilterQuery(filterParams));
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetUserById(long id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("by-phone/{phoneNumber}")]
    public async Task<IActionResult> GetUserByPhoneNumber(string phoneNumber)
    {
        var result = await _mediator.Send(new GetUserByPhoneNumberQuery(phoneNumber));
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
