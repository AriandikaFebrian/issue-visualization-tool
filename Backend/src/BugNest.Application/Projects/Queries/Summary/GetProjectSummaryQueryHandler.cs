using BugNest.Application.DTOs.Projects;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetProjectSummary;

public class GetProjectSummaryQueryHandler : IRequestHandler<GetProjectSummaryQuery, ProjectSummaryDto?>
{
    private readonly IProjectRepository _projectRepo;

    public GetProjectSummaryQueryHandler(IProjectRepository projectRepo)
    {
        _projectRepo = projectRepo;
    }

    public async Task<ProjectSummaryDto?> Handle(GetProjectSummaryQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepo.GetByCodeWithDetailsAsync(request.ProjectCode);
        if (project == null) return null;

        var issues = project.Issues;

        var statusCounts = issues
            .GroupBy(i => i.Status.ToString())
            .ToDictionary(g => g.Key, g => g.Count());

        return new ProjectSummaryDto
        {
            ProjectCode = project.ProjectCode!,
            TotalIssues = issues.Count,
            IssueStatusCounts = statusCounts,
            MemberCount = project.Members.Count,
            LastActivityAt = project.ActivityLogs
                .OrderByDescending(a => a.CreatedAt)
                .FirstOrDefault()?.CreatedAt
        };
    }
}
