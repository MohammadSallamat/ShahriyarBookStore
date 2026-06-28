using Application.CommonApplication;
using Application.UsersApp.Create;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Users.List_of_users;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetUserListQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("CreateUser")]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        var result = await _mediator.Send(command);

        return result.ToActionResult();

    }

}