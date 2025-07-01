using MediatR;
using BugNest.Application.Common.Dtos;
using BugNest.Application.Interfaces;
using BugNest.Application.DTOs.Projects;
using BugNest.Application.DTOs.Issues;
using BugNest.Domain.Enums;
using Application.Projects.Queries.GetProjectIssueDetail;

namespace BugNest.Application.Projects.Queries.GetProjectIssueDetail;

public class GetProjectIssueDetailQueryHandler : IRequestHandler<GetProjectIssueDetailQuery, ProjectIssueDetailDto?>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectIssueDetailQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectIssueDetailDto?> Handle(GetProjectIssueDetailQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetWithMembersAndOwnerByCodeAsync(request.ProjectCode);
        if (project == null) return null;

        var issues = await _projectRepository.GetIssuesByProjectCodeAsync(request.ProjectCode, cancellationToken);

        var statusCounts = issues
            .GroupBy(i => i.Status)
            .ToDictionary(g => g.Key.ToString(), g => g.Count());

        var resolved = issues.Count(i => i.Status == IssueStatus.Resolved);
        var total = issues.Count;
        var progress = total > 0 ? (double)resolved / total * 100 : 0;

        return new ProjectIssueDetailDto
        {
            ProjectCode = project.ProjectCode,
            Name = project.Name,
            Description = project.Description,
            RepositoryUrl = project.RepositoryUrl,
            DocumentationUrl = project.DocumentationUrl,
            Status = project.Status.ToString(),
            Visibility = project.Visibility.ToString(),
            CreatedAt = project.CreatedAt,
            UpdatedAt = project.UpdatedAt,

            Owner = new UserDto
            {
                NRP = project.Owner?.NRP ?? "",
                Username = project.Owner?.Username ?? "",
                FullName = project.Owner?.FullName ?? "",
                ProfilePictureUrl = project.Owner?.ProfilePictureUrl
            },

            Members = project.Members.Select(pm => new UserDto
            {
                NRP = pm.User.NRP,
                Username = pm.User.Username,
                FullName = pm.User.FullName,
                ProfilePictureUrl = pm.User.ProfilePictureUrl
            }).ToList(),

            TotalIssues = total,
            IssueStatusCounts = statusCounts,
            ProgressPercentage = progress,

            RecentIssues = issues
                .OrderByDescending(i => i.CreatedAt)
                .Take(5)
                .Select(i => new IssueDto
                {
                    Id = i.Id,
                    Title = i.Title,
                    Status = i.Status,
                    Priority = i.Priority,

                                        CreatedAt = i.CreatedAt,
                                        AssignedUsers = i.AssignedUsers.Select(u => new AssignedUserDto
                    {
                        UserId = u.User.Id,
                        NRP = u.User.NRP,
                        Username = u.User.Username,
                        FullName = u.User.FullName,
                        Email = u.User.Email,
                        Role = u.User.Role.ToString(),
                        Position = u.User.Position?.ToString(),
                        Department = u.User.Department?.ToString(),
                        ProfilePictureUrl = u.User.ProfilePictureUrl
                    }).ToList()

                }).ToList(),

            RecentIssueSummaries = issues
                .OrderByDescending(i => i.CreatedAt)
                .Take(5)
                .Select(i => i.Title)
                .ToList()
        };
    }
}
