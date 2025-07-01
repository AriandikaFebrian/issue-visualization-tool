using MediatR;
using BugNest.Application.Common.Dtos;
using BugNest.Domain.Entities;

namespace BugNest.Application.Users.Commands;

public class UpdateProfileCommand : IRequest<User?>
{
    public Guid UserId { get; }
    public UpdateProfileRequestDto Request { get; }

    public UpdateProfileCommand(Guid userId, UpdateProfileRequestDto request)
    {
        UserId = userId;
        Request = request;
    }
}
