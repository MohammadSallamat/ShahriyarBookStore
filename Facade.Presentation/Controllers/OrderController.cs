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
}
