using BugNest.Application.DTOs.Projects;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetProjectMembers;

public class GetProjectMembersQueryHandler : IRequestHandler<GetProjectMembersQuery, List<ProjectMemberDto>>
{
    private readonly IProjectMemberRepository _memberRepository;

    public GetProjectMembersQueryHandler(IProjectMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<List<ProjectMemberDto>> Handle(GetProjectMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _memberRepository.GetMembersByProjectIdAsync(request.ProjectId);

        return members.Select(m => new ProjectMemberDto
        {
            UserNRP = m.User?.NRP ?? "",
            Username = m.User?.Username ?? "",
            Email = m.User?.Email ?? "",
            Role = m.User?.Role.ToString() ?? ""
        }).ToList();
    }
}
