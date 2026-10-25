using Application.Categories.AddChild;
using Application.Categories.Create;
using Application.Categories.Edit;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Categories.GetById;
using Query.Categories.GetList;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var result = await _mediator.Send(new GetCategoryListQuery());
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetCategoryById(long id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("add-child")]
    public async Task<IActionResult> AddChild([FromBody] AddChildCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }
}
