// 📁 Application/UseCases/Users/Commands/RegisterUserCommand.cs
using MediatR;
using BugNest.Application.Common.Dtos;

namespace BugNest.Application.Users.Commands;

public class RegisterUserCommand : IRequest<RegisterUserResponseDto?>
{
    public RegisterUserDto Dto { get; set; }

    public RegisterUserCommand(RegisterUserDto dto)
    {
        Dto = dto;
    }
}

