using BugNest.Application.DTOs.Projects;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetProjectsByOwner;

public class GetProjectsByOwnerQueryHandler : IRequestHandler<GetProjectsByOwnerQuery, List<OwnerProjectDto>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectsByOwnerQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<OwnerProjectDto>> Handle(GetProjectsByOwnerQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetProjectsByOwnerAsync(request.OwnerId);

        return projects.Select(p => new OwnerProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            ProjectCode = p.ProjectCode,
            RepositoryUrl = p.RepositoryUrl,
            DocumentationUrl = p.DocumentationUrl,
            Status = p.Status,
            Visibility = p.Visibility,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,

            OwnerId = p.OwnerId,
            OwnerName = p.Owner?.Username ?? "-",
            OwnerEmail = p.Owner?.Email ?? "-",
            OwnerProfilePictureUrl = p.Owner?.ProfilePictureUrl,

            MemberCount = p.Members.Count,
            IssueCount = p.Issues.Count,

            Members = p.Members.Select(m => m.User?.Username ?? "Unknown").ToList(),
            Issues = p.Issues.Select(i => i.Title).ToList()
        }).ToList();
    }
}
