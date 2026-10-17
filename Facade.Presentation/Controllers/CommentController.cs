using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Comments.DTOs;
using Query.Comments.GetByFilter;
using Query.Comments.GetById;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;

    public CommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetComments([FromQuery] CommentFilterParams filterParams)
    {
        var result = await _mediator.Send(new GetCommentByFilterQuery(filterParams));
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetCommentById(long id)
    {
        var result = await _mediator.Send(new GetCommentByIdQuery(id));
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
