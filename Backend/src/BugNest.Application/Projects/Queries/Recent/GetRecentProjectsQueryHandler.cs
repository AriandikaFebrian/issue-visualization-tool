using BugNest.Application.DTOs.Projects;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetRecentProjects;

public class GetRecentProjectsQueryHandler : IRequestHandler<GetRecentProjectsQuery, List<RecentProjectDto>>
{
    private readonly IRecentProjectRepository _recentProjectRepository;
    private readonly IUserContext _userContext;

    public GetRecentProjectsQueryHandler(
        IRecentProjectRepository recentProjectRepository,
        IUserContext userContext)
    {
        _recentProjectRepository = recentProjectRepository;
        _userContext = userContext;
    }

    public async Task<List<RecentProjectDto>> Handle(GetRecentProjectsQuery request, CancellationToken cancellationToken)
    {
        var nrp = _userContext.GetNRP();
        if (string.IsNullOrWhiteSpace(nrp))
            throw new Exception("NRP tidak ditemukan dalam konteks user.");

        return await _recentProjectRepository.GetRecentProjectsByNRPAsync(nrp);
    }
}
