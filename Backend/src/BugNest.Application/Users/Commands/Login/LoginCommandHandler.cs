using BugNest.Application.Common.Dtos;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.Users.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public LoginCommandHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<LoginResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Identifier)
                 ?? await _userRepository.GetByNRPAsync(request.Identifier);

        if (user == null || !_authService.VerifyPassword(user.PasswordHash, request.Password))
            return null;

        return new LoginResponseDto
        {
            Token = _authService.GenerateJwtToken(user),
            Username = user.Username,
            Email = user.Email,
            Role = user.Role.ToString(),
            NRP = user.NRP,
            FullName = user.FullName,
            ProfilePictureUrl = user.ProfilePictureUrl,
            Department = user.Department?.ToString(),
            Position = user.Position?.ToString()
        };
    }
}
