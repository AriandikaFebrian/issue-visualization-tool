using BugNest.Application.DTOs.Projects;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetPublicProjectsFeed;

public class GetPublicProjectsFeedQueryHandler : IRequestHandler<GetPublicProjectsFeedQuery, List<PublicProjectDto>>
{
    private readonly IProjectRepository _projectRepository;

    public GetPublicProjectsFeedQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<PublicProjectDto>> Handle(GetPublicProjectsFeedQuery request, CancellationToken cancellationToken)
    {
        var projects = await _projectRepository.GetPublicProjectsWithOwnerAsync();

        return projects
            .Select(p => new PublicProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                ProjectCode = p.ProjectCode,
                RepositoryUrl = p.RepositoryUrl,
                UpdatedAt = p.UpdatedAt ?? p.CreatedAt,
                OwnerName = p.Owner?.FullName ?? "Unknown",
                ProfilePictureUrl = p.Owner?.ProfilePictureUrl
            })
            .ToList();
    }
}
