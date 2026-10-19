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
}
