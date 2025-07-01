using BugNest.Application.DTOs.Issues;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Queries;

public class GetIssuesByProjectQueryHandler : IRequestHandler<GetIssuesByProjectQuery, List<IssueDto>>
{
    private readonly IIssueRepository _issueRepository;
    private readonly IProjectRepository _projectRepository;

    public GetIssuesByProjectQueryHandler(
        IIssueRepository issueRepository,
        IProjectRepository projectRepository)
    {
        _issueRepository = issueRepository;
        _projectRepository = projectRepository;
    }

    public async Task<List<IssueDto>> Handle(GetIssuesByProjectQuery request, CancellationToken cancellationToken)
    {
        var projectId = await _projectRepository.GetProjectIdByCodeAsync(request.ProjectCode);
        if (projectId is null)
            throw new Exception($"Project with code '{request.ProjectCode}' not found.");

        var issues = await _issueRepository.GetIssuesWithDetailsByProjectIdAsync(projectId.Value);

        return issues.Select(i => new IssueDto
        {
            Id = i.Id,
            IssueCode = i.IssueCode,
            Title = i.Title,
            Status = i.Status,
            Priority = i.Priority,
            Description = i.Description,
            CreatedAt = i.CreatedAt,
            Deadline = i.Deadline,
            EstimatedFixHours = i.EstimatedFixHours,

            Creator = i.Creator is not null
                ? new CreatorUserDto
                {
                    UserId = i.Creator.Id,
                    NRP = i.Creator.NRP,
                    FullName = i.Creator.FullName ?? "",
                    Username = i.Creator.Username,
                    Email = i.Creator.Email,
                    Role = i.Creator.Role.ToString(),
                    ProfilePictureUrl = i.Creator.ProfilePictureUrl
                }
                : null,

            Tags = i.Tags.Select(t => new TagDto
            {
                Id = t.TagId,
                Name = t.Tag?.Name ?? "",
                    Color = t.Tag?.Color ?? "",
    Category = t.Tag?.Category,
            }).ToList(),

            AssignedUsers = i.AssignedUsers.Select(a => new AssignedUserDto
            {
                UserId = a.UserId,
                NRP = a.User?.NRP ?? "",
                FullName = a.User?.FullName ?? "",
                Username = a.User?.Username ?? "",
                Email = a.User?.Email ?? "",
                Role = a.User?.Role.ToString() ?? "",
                Position = a.User?.Position?.ToString(),
                Department = a.User?.Department?.ToString(),
                ProfilePictureUrl = a.User?.ProfilePictureUrl
            }).ToList()

        }).ToList();
    }
}
