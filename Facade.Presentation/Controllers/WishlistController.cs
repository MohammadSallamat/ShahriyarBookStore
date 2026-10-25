using Application.Wishlists.Add;
using Application.Wishlists.Remove;
using Facade.Presentation._Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Wishlists.GetByUserId;
using System.Threading.Tasks;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishlistController : ControllerBase
{
    private readonly IMediator _mediator;

    public WishlistController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId:long}")]
    public async Task<IActionResult> GetUserWishlist(long userId)
    {
        var result = await _mediator.Send(new GetUserWishlistQuery(userId));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddWishlistItem([FromBody] AddWishlistItemCommand command)
    {
        var result = await _mediator.Send(command);
        return result.ToActionResult();
    }

    [HttpDelete("{wishlistId:long}")]
    public async Task<IActionResult> RemoveWishlistItem(long wishlistId, [FromQuery] long userId)
    {
        var result = await _mediator.Send(new RemoveWishlistItemCommand(wishlistId, userId));
        return result.ToActionResult();
    }
}
