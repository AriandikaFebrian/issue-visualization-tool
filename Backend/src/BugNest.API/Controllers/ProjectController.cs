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
using BugNest.Infrastructure.Services;
using BugNest.Application.Common;

namespace BugNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IProjectRepository _projectRepository;

    private readonly IUserRepository _userRepository;
    private readonly IProjectSourceService _projectSourceService;
    private readonly IWebEnvironment _webEnv; // ← Tambahkan ini

    public ProjectController(
  IMediator mediator,
  IProjectRepository projectRepository,
  IUserRepository userRepository,
  IWebEnvironment webEnv, // ← Fix typo ini
  IProjectSourceService projectSourceService) // ← Tambahkan ini
    {
        _mediator = mediator;
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _projectSourceService = projectSourceService;
        _webEnv = webEnv;// ← Fix typo ini
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
    var userNrp = User.FindFirstValue("nrp");
    if (string.IsNullOrWhiteSpace(userNrp))
        return Unauthorized("NRP tidak ditemukan dalam klaim.");

    // Simpan proyek ke daftar terbaru user
    await _mediator.Send(new AddOrUpdateRecentProjectCommand(userNrp, projectCode));

    // Ambil detail proyek lengkap beserta ringkasan
    var detailResult = await _mediator.Send(new GetProjectIssueDetailQuery(projectCode));
    if (detailResult == null)
        return NotFound("Project tidak ditemukan.");

    return Ok(detailResult);
}


    [HttpPost("{projectId}/upload-source")]
    [RequestSizeLimit(100_000_000)] // 100 MB limit
    public async Task<IActionResult> UploadSource(Guid projectId, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File tidak valid.");

        try
        {
            var path = await _projectSourceService.UploadAndExtractSourceAsync(projectId, file);
            return Ok(new { message = "Berhasil upload dan ekstrak source.", path });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

[HttpGet("{projectCode}/source-tree")]
public async Task<IActionResult> GetProjectSourceTreeByCode(string projectCode)
{
    var project = await _projectRepository.GetByCodeAsync(projectCode); // ← gunakan projectCode
    if (project == null || string.IsNullOrEmpty(project.SourceUploadPath))
        return NotFound("Source belum diupload.");

    var rootPath = Path.Combine(_webEnv.WebRootPath ?? "wwwroot", project.SourceUploadPath);

    if (!Directory.Exists(rootPath))
        return NotFound("Folder tidak ditemukan.");

    var tree = BuildDirectoryTree(rootPath, rootPath); // rootPath as base
    return Ok(tree);
}



    private object BuildDirectoryTree(string path, string basePath)
    {
        var name = Path.GetFileName(path);
        var relativePath = Path.GetRelativePath(basePath, path).Replace("\\", "/");

        var node = new
        {
            name,
            path = relativePath,
            type = "folder",
            children = Directory.GetDirectories(path)
                .Select(dir => BuildDirectoryTree(dir, basePath))
                .Concat(Directory.GetFiles(path).Select(file => new
                {
                    name = Path.GetFileName(file),
                    path = Path.GetRelativePath(basePath, file).Replace("\\", "/"),
                    type = "file"
                }))
        };

        return node;
    }

[HttpGet("{projectCode}/source-file")]
public async Task<IActionResult> GetSourceFile(string projectCode, [FromQuery] string path)
{
    if (string.IsNullOrWhiteSpace(path))
        return BadRequest("Path tidak boleh kosong.");

    try
    {
        var project = await _projectRepository.GetByCodeAsync(projectCode);
        if (project == null)
            return NotFound("Project tidak ditemukan.");

        var content = await _projectSourceService.GetFileContentAsync(project.Id, path);
        return Ok(new { path, content });
    }
    catch (FileNotFoundException)
    {
        return NotFound("File tidak ditemukan.");
    }
    catch (Exception ex)
    {
        return BadRequest(new { error = ex.Message });
    }
}







}
