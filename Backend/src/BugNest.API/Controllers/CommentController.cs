using BugNest.API.Requests.Comments;
using BugNest.Application.DTOs.Comments;
using BugNest.Application.UseCases.Comments;
using BugNest.Application.UseCases.Comments.Commands;
using BugNest.Application.UseCases.Comments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommentsController : ControllerBase
{
       private readonly IMediator _mediator;

    public CommentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("issue/{issueCode}")]
    public async Task<IActionResult> GetByIssue(string issueCode)
    {
        var query = new GetCommentsByIssueQuery(issueCode);
        var comments = await _mediator.Send(query);
        return Ok(comments);
    }


    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
    {
        var nrp = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(nrp))
            return Unauthorized("NRP tidak ditemukan dalam token.");

        var command = new CreateCommentCommand(request.IssueCode, request.Content, nrp);

        CommentDto result = await _mediator.Send(command);
        return Ok(result);
    }

}