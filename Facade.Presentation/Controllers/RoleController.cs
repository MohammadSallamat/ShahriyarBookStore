using Application.Roles.Create;
using Application.Roles.Edit;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Roles.GetById;
using Query.Roles.GetList;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _mediator.Send(new GetRoleListQuery());
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetRoleById(long id)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }
}
