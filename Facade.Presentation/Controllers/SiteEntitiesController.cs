using Application.SiteEntities.Banners.Create;
using Application.SiteEntities.Banners.Edit;
using Application.SiteEntities.Sliders.Create;
using Application.SiteEntities.Sliders.Edit;
using Facade.Presentation._Utils;
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

    [HttpPost("sliders")]
    public async Task<IActionResult> CreateSlider([FromForm] CreateSliderCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("sliders")]
    public async Task<IActionResult> EditSlider([FromForm] EditSliderCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPost("banners")]
    public async Task<IActionResult> CreateBanner([FromForm] CreateBannerCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpPut("banners")]
    public async Task<IActionResult> EditBanner([FromForm] EditBannerCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }
}
