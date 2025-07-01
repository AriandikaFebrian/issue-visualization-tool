// 📁 Application/Projects/Commands/AddOrUpdateRecentProject/AddOrUpdateRecentProjectCommandHandler.cs
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.Projects.Commands.AddOrUpdateRecentProject;

public class AddOrUpdateRecentProjectCommandHandler : IRequestHandler<AddOrUpdateRecentProjectCommand, Unit> // ✅ Tambahkan , Unit
{
    private readonly IRecentProjectRepository _recentProjectRepository;

    public AddOrUpdateRecentProjectCommandHandler(IRecentProjectRepository recentProjectRepository)
    {
        _recentProjectRepository = recentProjectRepository;
    }

    public async Task<Unit> Handle(AddOrUpdateRecentProjectCommand request, CancellationToken cancellationToken)
    {
        await _recentProjectRepository.AddOrUpdateRecentProjectAsync(request.UserNRP, request.ProjectCode);
        return Unit.Value;
    }
}
