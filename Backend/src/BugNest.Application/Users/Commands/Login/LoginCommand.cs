using BugNest.Application.Common.Dtos;
using MediatR;

namespace BugNest.Application.Users.Commands;

public class LoginCommand : IRequest<LoginResponseDto?>
{
    public string Identifier { get; set; } = default!;
    public string Password { get; set; } = default!;
}
