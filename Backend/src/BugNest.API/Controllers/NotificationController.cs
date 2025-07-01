using BugNest.Application.UseCases.Notifications.Commands;
using BugNest.Application.UseCases.Notifications.Queries;
using BugNest.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;

    public NotificationController(IMediator mediator, IUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var nrp = await GetCurrentUserNRP();
        if (nrp == null)
            return Unauthorized("NRP tidak ditemukan dari token.");

        var result = await _mediator.Send(new GetNotificationsQuery(nrp, onlyUnread: false));
        return Ok(result);
    }
    [HttpGet("unread")]
    public async Task<IActionResult> GetUnread()
    {
        var nrp = await GetCurrentUserNRP();
        if (nrp == null)
            return Unauthorized("NRP tidak ditemukan dari token.");

        var result = await _mediator.Send(new GetNotificationsQuery(nrp, onlyUnread: true));
        return Ok(result);
    }
    [HttpPatch("{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var userId = GetUserIdFromToken();
        if (userId == null)
            return Unauthorized();

        await _mediator.Send(new MarkNotificationAsReadCommand(id, userId.Value));
        return Ok(new { message = "Notification marked as read" });
    }
    [HttpPatch("read-all")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        var userId = GetUserIdFromToken();
        if (userId == null)
            return Unauthorized();

        await _mediator.Send(new MarkAllNotificationsAsReadCommand(userId.Value));
        return Ok(new { message = "All notifications marked as read" });
    }
    private Guid? GetUserIdFromToken()
    {
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Guid.TryParse(userIdStr, out var userId) ? userId : null;
    }
    private async Task<string?> GetCurrentUserNRP()
    {
        var userId = GetUserIdFromToken();
        if (userId == null)
            return null;

        var user = await _userRepository.GetByIdAsync(userId.Value);
        return user?.NRP;
    }
}
