using BugNest.Application.DTOs.Projects;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetAllProjectSummaries;

public class GetAllProjectSummariesQueryHandler : IRequestHandler<GetAllProjectSummariesQuery, List<ProjectSummaryDto>>
{
    private readonly IProjectRepository _projectRepo;
    private readonly IUserRepository _userRepo;
    private readonly IUserContext _userContext;

    public GetAllProjectSummariesQueryHandler(
        IProjectRepository projectRepo,
        IUserRepository userRepo,
        IUserContext userContext)
    {
        _projectRepo = projectRepo;
        _userRepo = userRepo;
        _userContext = userContext;
    }

    public async Task<List<ProjectSummaryDto>> Handle(GetAllProjectSummariesQuery request, CancellationToken cancellationToken)
    {
        var nrp = _userContext.GetNRP();
        var owner = await _userRepo.GetByNRPAsync(nrp);
        if (owner is null) throw new Exception("User tidak ditemukan");

        var projects = await _projectRepo.GetProjectsByOwnerAsync(owner.Id);

        return projects.Select(project =>
        {
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
        }).ToList();
    }
}
