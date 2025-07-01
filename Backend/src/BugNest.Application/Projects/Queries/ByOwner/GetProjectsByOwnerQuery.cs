using BugNest.Application.DTOs.Projects;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetProjectsByOwner;

public class GetProjectsByOwnerQuery : IRequest<List<OwnerProjectDto>>
{
    public Guid OwnerId { get; }

    public GetProjectsByOwnerQuery(Guid ownerId)
    {
        OwnerId = ownerId;
    }
}
