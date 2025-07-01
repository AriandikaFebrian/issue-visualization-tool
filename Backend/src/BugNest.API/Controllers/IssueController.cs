using BugNest.Application.DTOs.Issues;
using BugNest.Application.UseCases.Issues.Commands;
using BugNest.Application.UseCases.Issues.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BugNest.Application.Interfaces;
using System.Security.Claims;

namespace BugNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class IssueController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContext _userContext;

    public IssueController(IMediator mediator, IUserContext userContext)
    {
        _mediator = mediator;
        _userContext = userContext;
    }

[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateIssueDto dto)
{
    var nrp = _userContext.GetNRP();
    var command = new CreateIssueCommand(dto, nrp);
    var result = await _mediator.Send(command);
    return Ok(result);
}



    [HttpGet("by-code/{projectCode}")]
    public async Task<IActionResult> GetIssuesByProjectCode(string projectCode)
    {
        var result = await _mediator.Send(new GetIssuesByProjectQuery(projectCode));
        return Ok(result);
    }

    [HttpPut("{issueCode}/assign-users")]
    public async Task<IActionResult> AssignUsers(string issueCode, [FromBody] AssignUsersDto dto)
    {
        if (dto.NRPs == null || !dto.NRPs.Any())
            return BadRequest(new { message = "No NRPs provided." });

        await _mediator.Send(new AssignUsersToIssueCommand(issueCode, dto.NRPs));
        return Ok(new { message = "Users assigned successfully." });
    }

    [HttpGet("{issueCode}/history")]
    public async Task<IActionResult> GetHistory(string issueCode)
    {
        var histories = await _mediator.Send(new GetIssueHistoryQuery(issueCode));
        return Ok(histories);
    }

    [HttpPatch("{issueCode}/status")]
    public async Task<IActionResult> ChangeStatus(string issueCode, [FromBody] ChangeIssueStatusDto dto)
    {
        var nrp = _userContext.GetNRP();
        await _mediator.Send(new ChangeIssueStatusCommand(issueCode, dto.NewStatus, nrp, dto.Note));
        return Ok(new { message = "Issue status updated successfully" });
    }

 [HttpGet("recent")]
        public async Task<IActionResult> GetRecentIssues([FromQuery] int count = 5)
        {
            var query = new GetRecentIssuesQuery(count);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

     [HttpGet("assigned")]
        public async Task<IActionResult> GetAssignedIssues()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("User ID not found in token.");

            if (!Guid.TryParse(userIdClaim.Value, out var userId))
                return BadRequest("Invalid user ID format.");

            var query = new GetAssignedIssuesQuery(userId);
            List<IssueDto> assignedIssues = await _mediator.Send(query);

            return Ok(assignedIssues);
        }
}
