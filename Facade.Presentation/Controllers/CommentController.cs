using Application.Comments.ChangeStatus;
using Application.Comments.Create;
using Application.Comments.Edit;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] EditCommentCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("status")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeCommentStatusCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }
}
