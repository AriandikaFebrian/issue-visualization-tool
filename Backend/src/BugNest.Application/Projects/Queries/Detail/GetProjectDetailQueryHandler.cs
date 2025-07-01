using BugNest.Application.DTOs.Projects;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetProjectDetail;

public class GetProjectDetailQueryHandler : IRequestHandler<GetProjectDetailQuery, ProjectDetailDto?>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectDetailQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ProjectDetailDto?> Handle(GetProjectDetailQuery request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByCodeWithOwnerAsync(request.ProjectCode);
        if (project == null) return null;

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
            OwnerFullName = project.Owner?.FullName
        };
    }
}
