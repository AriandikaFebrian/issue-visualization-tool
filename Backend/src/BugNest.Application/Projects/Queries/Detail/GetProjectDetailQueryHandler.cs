using BugNest.Application.DTOs.Projects;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetProjectDetail;

public class GetProjectDetailQueryHandler : IRequestHandler<GetProjectDetailQuery, ProjectDetailDto?>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IIssueRepository _issueRepository;

    public GetProjectDetailQueryHandler(IProjectRepository projectRepository, IIssueRepository issueRepository)
    {
        _projectRepository = projectRepository;
        _issueRepository = issueRepository;
    }

    public async Task<ProjectDetailDto?> Handle(GetProjectDetailQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByCodeWithOwnerAsync(request.ProjectCode);
        if (project == null) return null;

        // Ambil issue-issue terkait
        var issues = await _issueRepository.GetByProjectIdAsync(project.Id);


        var issueStatusCounts = issues
            .GroupBy(i => i.Status.ToString())
            .ToDictionary(g => g.Key, g => g.Count());

        var totalIssues = issues.Count;

        // Ambil 3 issue terbaru (bisa disesuaikan dengan kebutuhan)
        var recentIssueSummaries = issues
            .OrderByDescending(i => i.CreatedAt)
            .Take(3)
            .Select(i => i.Title)
            .ToList();

        // Ambil last activity
        var lastActivity = issues
            .OrderByDescending(i => i.UpdatedAt ?? i.CreatedAt)
            .FirstOrDefault()?.UpdatedAt ?? issues.OrderByDescending(i => i.CreatedAt).FirstOrDefault()?.CreatedAt;

        // Ambil jumlah member
        var memberCount = project.Members?.Count ?? 0;

        return new ProjectDetailDto
        {
            Name = project.Name,
            Description = project.Description,
            ProjectCode = project.ProjectCode,
            RepositoryUrl = project.RepositoryUrl,
            DocumentationUrl = project.DocumentationUrl,
            Status = project.Status.ToString(),
            Visibility = project.Visibility.ToString(),
            UpdatedAt = project.UpdatedAt,
            OwnerNRP = project.Owner?.NRP ?? "",
            OwnerUsername = project.Owner?.Username ?? "",
            OwnerFullName = project.Owner?.FullName,

            // === Tambahan Summary ===
            TotalIssues = totalIssues,
            IssueStatusCounts = issueStatusCounts,
            LastActivityAt = lastActivity,
            MemberCount = memberCount,
            RecentIssueSummaries = recentIssueSummaries
        };
    }
}
