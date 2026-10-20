using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.SiteEntities.GetBanners;
using Query.SiteEntities.GetSliders;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SiteEntitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SiteEntitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("sliders")]
    public async Task<IActionResult> GetSliders()
    {
        var result = await _mediator.Send(new GetSlidersQuery());
        return Ok(result);
    }

    [HttpGet("banners")]
    public async Task<IActionResult> GetBanners()
    {
        var result = await _mediator.Send(new GetBannersQuery());
        return Ok(result);
    }
}
