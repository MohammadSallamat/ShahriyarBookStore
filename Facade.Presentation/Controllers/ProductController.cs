using Application.Products.AddImage;
using Application.Products.Create;
using Application.Products.Edit;
using Application.Products.RemoveImage;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Products.DTOs;
using Query.Products.GetByFilter;
using Query.Products.GetById;
using Query.Products.GetBySlug;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductFilterParams filterParams)
    {
        var result = await _mediator.Send(new GetProductByFilterQuery(filterParams));
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetProductById(long id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("by-slug/{slug}")]
    public async Task<IActionResult> GetProductBySlug(string slug)
    {
        var result = await _mediator.Send(new GetProductBySlugQuery(slug));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateProducteCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromForm] EditProductcommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("images")]
    public async Task<IActionResult> AddImage([FromForm] AddImageProductCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete("images")]
    public async Task<IActionResult> RemoveImage([FromBody] RemoveImageProductCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }
}
