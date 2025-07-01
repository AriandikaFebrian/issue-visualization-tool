using BugNest.Application.DTOs.Tags;
using BugNest.Application.UseCases.Tags.Commands;
using BugNest.Application.UseCases.Tags.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TagController : ControllerBase
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagDto dto)
    {
        var nrp = User.FindFirst("nrp")?.Value;
        if (string.IsNullOrWhiteSpace(nrp))
            return Unauthorized("NRP tidak ditemukan di token.");

        dto.CreatedByNRP = nrp;

        var tagId = await _mediator.Send(new CreateTagCommand(dto));
        return Ok(new { TagId = tagId });
    }

    [HttpGet]
public async Task<IActionResult> GetAllTags([FromQuery] string? projectCode)
{
    var tags = await _mediator.Send(new GetAllTagsQuery(projectCode));
    return Ok(tags);
}

}
