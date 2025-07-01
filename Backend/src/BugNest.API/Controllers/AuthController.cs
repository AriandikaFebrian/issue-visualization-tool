// 📁 BugNest.API/Controllers/AuthController.cs

using BugNest.Application.Common.Dtos;
using BugNest.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MediatR;
using BugNest.Application.Users.Commands;

namespace BugNest.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepository;

    public AuthController(IMediator mediator, IUserRepository userRepository)
    {
        _mediator = mediator;
        _userRepository = userRepository;
    }

    // ✅ REGISTER
   [HttpPost("register")]
[AllowAnonymous]
public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
{
    var result = await _mediator.Send(new RegisterUserCommand(dto));

    if (result == null)
        return BadRequest(new { Message = "Email already registered." });

    return Ok(result);
}


    // ✅ LOGIN
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);

        if (result == null)
            return Unauthorized("Login gagal: Email/NRP atau password salah.");

        return Ok(result);
    }

    // ✅ GET CURRENT USER PROFILE
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.Id,
            user.Username,
            user.Email,
            user.Role,
            user.NRP,
            user.FullName,
            user.ProfilePictureUrl,
            user.PhoneNumber,
            user.Department,
            user.Position
        });
    }

    // ✅ UPDATE PROFILE
    [HttpPut("me")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto request)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        try
        {
            var updatedUser = await _mediator.Send(new UpdateProfileCommand(userId, request));
            if (updatedUser == null)
                return NotFound("User tidak ditemukan.");

            return Ok(new
            {
                updatedUser.Id,
                updatedUser.Username,
                updatedUser.Email,
                updatedUser.NRP,
                updatedUser.FullName,
                updatedUser.ProfilePictureUrl,
                updatedUser.PhoneNumber,
                updatedUser.Department,
                updatedUser.Position
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // ✅ UPLOAD PROFILE PICTURE
    [HttpPost("upload")]
    [Authorize]
    public async Task<IActionResult> UploadProfilePicture(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("File kosong.");

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        if (!Directory.Exists(uploadFolder))
            Directory.CreateDirectory(uploadFolder);

        var filePath = Path.Combine(uploadFolder, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
        return Ok(new { url = fileUrl });
    }
}
