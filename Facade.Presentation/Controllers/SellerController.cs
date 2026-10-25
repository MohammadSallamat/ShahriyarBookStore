using Application.Sellers.AddInventory;
using Application.Sellers.ChangeStatus;
using Application.Sellers.Create;
using Application.Sellers.Edit;
using Application.Sellers.EditInventory;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Sellers.GetById;
using Query.Sellers.GetList;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SellerController : ControllerBase
{
    private readonly IMediator _mediator;

    public SellerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetSellers()
    {
        var result = await _mediator.Send(new GetSellerListQuery());
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetSellerById(long id)
    {
        var result = await _mediator.Send(new GetSellerByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSellerCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditSellerCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("status")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeSellerStatusCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("inventories")]
    public async Task<IActionResult> AddInventory([FromBody] AddSellerInventoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("inventories")]
    public async Task<IActionResult> EditInventory([FromBody] EditSellerInventoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }
}
