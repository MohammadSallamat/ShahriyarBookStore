using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Categories.GetById;
using Query.Categories.GetBySlug;
using Query.Categories.GetChildsByParentId;
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

    [HttpGet("by-slug/{slug}")]
    public async Task<IActionResult> GetCategoryBySlug(string slug)
    {
        var result = await _mediator.Send(new GetCategoryBySlugQuery(slug));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("{parentId:long}/childs")]
    public async Task<IActionResult> GetCategoryChilds(long parentId)
    {
        var result = await _mediator.Send(new GetCategoryChildsByParentIdQuery(parentId));
        return Ok(result);
    }
}
