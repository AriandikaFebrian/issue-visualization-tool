using BugNest.Application.UseCases.ActivityLogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugNest.API.Controllers;

[ApiController]
[Route("api/activities")]
[Authorize]
public class ActivityLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public ActivityLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetActivities([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _mediator.Send(new GetActivityLogsQuery(page, pageSize));
        return Ok(result);
    }

     [HttpGet("{id}")]
    public async Task<ActionResult<ActivityLogDetailDto>> GetActivityLogDetail(Guid id)
    {
        var result = await _mediator.Send(new GetActivityLogDetailByIdQuery(id));

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
