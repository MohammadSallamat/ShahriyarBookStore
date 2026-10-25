using Application.Discounts.Create;
using Application.Discounts.Delete;
using Application.Discounts.Edit;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Discounts.DTOs;
using Query.Discounts.GetByCode;
using Query.Discounts.GetByFilter;
using Query.Discounts.GetById;
using System.Threading.Tasks;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private readonly IMediator _mediator;

    public DiscountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetDiscounts([FromQuery] DiscountFilterParams filterParams)
    {
        var result = await _mediator.Send(new GetDiscountsByFilterQuery(filterParams));
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetDiscountById(long id)
    {
        var result = await _mediator.Send(new GetDiscountByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("by-code/{code}")]
    public async Task<IActionResult> GetDiscountByCode(string code)
    {
        var result = await _mediator.Send(new GetDiscountByCodeQuery(code));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDiscountCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditDiscountCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _mediator.Send(new DeleteDiscountCommand(id));
        return result.ToActionResult();
    }
}
