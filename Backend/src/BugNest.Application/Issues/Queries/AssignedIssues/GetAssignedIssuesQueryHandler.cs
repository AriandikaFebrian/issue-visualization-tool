using BugNest.Application.DTOs.Issues;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Queries;

public class GetAssignedIssuesQueryHandler : IRequestHandler<GetAssignedIssuesQuery, List<IssueDto>>
{
    private readonly IIssueRepository _issueRepository;

    public GetAssignedIssuesQueryHandler(IIssueRepository issueRepository)
    {
        _issueRepository = issueRepository;
    }

    public async Task<List<IssueDto>> Handle(GetAssignedIssuesQuery request, CancellationToken cancellationToken)
    {
        var issues = await _issueRepository.GetIssuesAssignedToUserAsync(request.UserId);

        if (issues == null || !issues.Any())
            return new List<IssueDto>();

        var issueDtos = issues.Select(issue => new IssueDto
        {
            Id = issue.Id,
            IssueCode = issue.IssueCode,
            Title = issue.Title,
            Status = issue.Status,
            Priority = issue.Priority,
            Description = issue.Description,
            CreatedAt = issue.CreatedAt,
            Deadline = issue.Deadline,
            EstimatedFixHours = issue.EstimatedFixHours,
            ProjectCode = issue.Project?.ProjectCode ?? "-",

            Creator = issue.Creator == null ? null : new CreatorUserDto
            {
                UserId = issue.Creator.Id,
                NRP = issue.Creator.NRP,
                FullName = issue.Creator.FullName ?? "",
                Username = issue.Creator.Username,
                Email = issue.Creator.Email,
                Role = issue.Creator.Role.ToString(),
                ProfilePictureUrl = issue.Creator.ProfilePictureUrl
            },

            Tags = issue.Tags
                .Where(it => it.Tag != null)
                .Select(it => new TagDto
                {
                    Id = it.Tag!.Id,
                    Name = it.Tag.Name,
                    Color = it.Tag.Color
                }).ToList(),

            AssignedUsers = issue.AssignedUsers.Select(au => new AssignedUserDto
            {
                UserId = au.User.Id,
                NRP = au.User.NRP,
                FullName = au.User.FullName ?? "",
                Username = au.User.Username,
                Email = au.User.Email,
                Role = au.User.Role.ToString(),
                Position = au.User.Position?.ToString(),
                Department = au.User.Department?.ToString(),
                ProfilePictureUrl = au.User.ProfilePictureUrl
            }).ToList()

        }).ToList();

        return issueDtos;
    }
}
