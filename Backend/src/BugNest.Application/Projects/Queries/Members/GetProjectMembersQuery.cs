using BugNest.Application.DTOs.Projects;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetProjectMembers;

public class GetProjectMembersQuery : IRequest<List<ProjectMemberDto>>
{
    public Guid ProjectId { get; }

    public GetProjectMembersQuery(Guid projectId)
    {
        ProjectId = projectId;
    }
}
