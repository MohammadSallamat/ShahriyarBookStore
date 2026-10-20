using Application.CommonApplication;
using Application.Sellers.ChangeStatus;
using Domain.SellerAggregate.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Query.Admin.GetCategoryStats;
using Query.Admin.GetDashboardSummary;
using Query.Admin.GetSellerInspection;

namespace Facade.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> GetDashboardSummary()
    {
        var result = await _mediator.Send(new GetAdminDashboardSummaryQuery());
        return Ok(result);
    }

    [HttpGet("category-stats")]
    public async Task<IActionResult> GetCategoryStats()
    {
        var result = await _mediator.Send(new GetCategoryStatsQuery());
        return Ok(result);
    }

    [HttpGet("sellers/{sellerId:long}/inspect")]
    public async Task<IActionResult> InspectSeller(long sellerId)
    {
        var result = await _mediator.Send(new GetSellerInspectionQuery(sellerId));
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPut("sellers/{sellerId:long}/status")]
    public async Task<IActionResult> ChangeSellerStatus(long sellerId, [FromBody] SellerStatus status)
    {
        var result = await _mediator.Send(new ChangeSellerStatusCommand(sellerId, status));
        if (result.Status == OperationResultStatus.Success)
            return Ok(result);

        if (result.Status == OperationResultStatus.NotFound)
            return NotFound(result.Message);

        return BadRequest(result.Message);
    }
}
