using Application.Categories.AddChild;
using Application.Categories.Create;
using Application.Categories.Edit;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Categories.DTOs;
using Query.Categories.GetById;
using Query.Categories.GetByParentId;
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
    public async Task<ActionResult<List<CategoryDto>>> GetCategories()
    {
        var result = await _mediator.Send(new GetCategoriesListQuery());
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(long id)
    {
        var result = await _mediator.Send(new GetCategoryByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("GetByParentId/{parentId:long}")]
    public async Task<ActionResult<List<CategoryDto>>> GetCategoriesByParentId(long parentId)
    {
        var result = await _mediator.Send(new GetCategoryByParentIdQuery(parentId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("AddChild")]
    public async Task<IActionResult> AddChildCategory(AddChildCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> EditCategory(EditCategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }
}
