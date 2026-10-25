using Application.UsersApp.AddAdress;
using Application.UsersApp.ChargeWallet;
using Application.UsersApp.Create;
using Application.UsersApp.DeleteAdress;
using Application.UsersApp.Edit;
using Application.UsersApp.EditAddress;
using Application.UsersApp.RegisterUser;
using Facade.Presentation._Utils;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromForm] EditUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("addresses")]
    public async Task<IActionResult> AddAddress([FromBody] AddAddressUserCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("addresses")]
    public async Task<IActionResult> EditAddress([FromBody] EditUserAddressCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete("addresses")]
    public async Task<IActionResult> DeleteAddress([FromBody] DeleteUserAddressCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("charge-wallet")]
    public async Task<IActionResult> ChargeWallet([FromBody] ChargeUserWalletCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }
}
