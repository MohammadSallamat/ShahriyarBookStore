using Application.Orders.AddItem;
using Application.Orders.CheckOut;
using Application.Orders.DecreaseItemCount;
using Application.Orders.IncreaseItemCount;
using Application.Orders.Remove;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Orders.GetById;
using Query.Orders.GetByUserId;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetOrderById(long id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("user/{userId:long}")]
    public async Task<IActionResult> GetOrdersByUserId(long userId)
    {
        var result = await _mediator.Send(new GetOrdersByUserIdQuery(userId));
        return Ok(result);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddItem([FromBody] AddOrderItemCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromBody] CheckoutOrderCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("items/increase")]
    public async Task<IActionResult> IncreaseItemCount([FromBody] IncreaseOrderItemCountCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("items/decrease")]
    public async Task<IActionResult> DecreaseItemCount([FromBody] DecreaseOrderItemCountCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete("items")]
    public async Task<IActionResult> RemoveItem([FromBody] RemoveOrderItemCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }
}
