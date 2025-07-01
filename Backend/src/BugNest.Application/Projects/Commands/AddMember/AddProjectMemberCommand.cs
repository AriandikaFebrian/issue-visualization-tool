using BugNest.Application.DTOs.Projects;
using MediatR;

namespace BugNest.Application.Projects.Commands.AddProjectMember;

public class AddProjectMemberCommand : IRequest<Unit>
{
    public AddMemberDto Dto { get; }

    public AddProjectMemberCommand(AddMemberDto dto)
    {
        Dto = dto;
    }
}

