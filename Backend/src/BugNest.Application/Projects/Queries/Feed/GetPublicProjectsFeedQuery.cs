using BugNest.Application.DTOs.Projects;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetPublicProjectsFeed;

public class GetPublicProjectsFeedQuery : IRequest<List<PublicProjectDto>>
{
}
