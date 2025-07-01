using BugNest.Application.DTOs.Projects;
using BugNest.Application.Projects.Commands.CreateProject;
using BugNest.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BugNest.Application.Common.Dtos;
using BugNest.Application.Projects.Queries.GetProjectsByOwner;
using BugNest.Application.Projects.Commands.AddProjectMember;
using BugNest.Application.Projects.Queries.GetProjectMembers;
using BugNest.Application.Projects.Queries.GetProjectSummary;
using BugNest.Application.Projects.Queries.GetProjectDetail;
using BugNest.Application.Projects.Queries.GetRecentProjects;
using BugNest.Application.Projects.Queries.GetAllProjectSummaries;
using BugNest.Application.Projects.Queries.GetPublicProjectsFeed;
using BugNest.Application.Projects.Commands.AddOrUpdateRecentProject;
using Application.Projects.Queries.GetProjectIssueDetail;

namespace BugNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IProjectRepository _projectRepository;

    private readonly IUserRepository _userRepository;

public ProjectController(IMediator mediator, IProjectRepository projectRepository, IUserRepository userRepository)
{
    _mediator = mediator;
    _projectRepository = projectRepository;
    _userRepository = userRepository;
}


[HttpPost]
public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
{
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        return Unauthorized();

    var user = await _userRepository.GetByIdAsync(userId);
    if (user == null)
        return Unauthorized("User tidak ditemukan.");

    var nrp = user.NRP;

    var command = new CreateProjectCommand(dto, nrp);
    var result = await _mediator.Send(command);

    return Ok(new { ProjectId = result });
}



    [HttpGet("mine")]
    public async Task<IActionResult> GetMyProjects()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _mediator.Send(new GetProjectsByOwnerQuery(Guid.Parse(userId)));
        return Ok(result);
    }

    [HttpPost("{projectCode}/members")]
    public async Task<IActionResult> AddMember(string projectCode, [FromBody] AddMemberDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var project = await _projectRepository.GetByCodeAsync(projectCode);
        if (project is null) return NotFound("Project not found.");

        if (project.OwnerId != Guid.Parse(userId))
            return Forbid("Only the project owner can add members.");

        dto.ProjectCode = projectCode;
        await _mediator.Send(new AddProjectMemberCommand(dto));
        return Ok(new { message = "Member added to project." });
    }

    [HttpGet("{projectCode}/members")]
    public async Task<IActionResult> GetMembers(string projectCode)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var project = await _projectRepository.GetByCodeAsync(projectCode);
        if (project is null) return NotFound("Project not found.");

        var result = await _mediator.Send(new GetProjectMembersQuery(project.Id));
        return Ok(result);
    }

    [HttpGet("{projectCode}/summary")]
    public async Task<IActionResult> GetProjectSummary(string projectCode)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();

        var result = await _mediator.Send(new GetProjectSummaryQuery(projectCode));
        if (result is null) return NotFound("Project not found.");

        return Ok(result);
    }

[HttpGet("{projectCode}")]
public async Task<IActionResult> GetDetail(string projectCode)
{
    var userNrp = User.FindFirstValue("nrp");
    if (string.IsNullOrWhiteSpace(userNrp))
        return Unauthorized("NRP tidak ditemukan dalam klaim.");

    // 1. Ambil detail project
    var result = await _mediator.Send(new GetProjectDetailQuery(projectCode, userNrp));
    if (result == null) return NotFound("Project tidak ditemukan.");

    // 2. Simpan ke recent
    await _mediator.Send(new AddOrUpdateRecentProjectCommand(userNrp, projectCode));

    return Ok(result);
}


[HttpGet("recent")]
public async Task<IActionResult> GetRecent()
{
    var result = await _mediator.Send(new GetRecentProjectsQuery());
    return Ok(result);
}


[HttpGet("summaries")]
public async Task<IActionResult> GetProjectSummaries()
{
    var result = await _mediator.Send(new GetAllProjectSummariesQuery());
    return Ok(result);
}



    [HttpGet("projects/public-feed")]
    public async Task<IActionResult> GetPublicProjectsFeed()
    {
        var result = await _mediator.Send(new GetPublicProjectsFeedQuery());
        return Ok(result);
    }
[HttpGet("{projectCode}/details")]
public async Task<ActionResult<ProjectIssueDetailDto>> GetProjectIssueDetail(string projectCode)
{
    var result = await _mediator.Send(new GetProjectIssueDetailQuery(projectCode));

    if (result == null)
        return NotFound();

    return Ok(result);
}



}
