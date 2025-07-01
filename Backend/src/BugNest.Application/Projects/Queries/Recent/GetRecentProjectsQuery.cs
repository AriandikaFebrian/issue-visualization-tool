using MediatR;
using BugNest.Application.DTOs.Projects;

namespace BugNest.Application.Projects.Queries.GetRecentProjects;

public class GetRecentProjectsQuery : IRequest<List<RecentProjectDto>>
{
}
