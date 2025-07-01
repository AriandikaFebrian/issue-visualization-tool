using BugNest.Application.Projects.Commands.AddProjectMember;
using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using MediatR;

namespace BugNest.Application.Projects.Commands.AddProjectMember;

public class AddProjectMemberCommandHandler : IRequestHandler<AddProjectMemberCommand, Unit>
{
    private readonly IProjectMemberRepository _memberRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly INotificationService _notificationService;

    public AddProjectMemberCommandHandler(
        IProjectMemberRepository memberRepository,
        IUserRepository userRepository,
        IProjectRepository projectRepository,
        INotificationService notificationService)
    {
        _memberRepository = memberRepository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _notificationService = notificationService;
    }

    public async Task<Unit> Handle(AddProjectMemberCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var user = await _userRepository.GetByNRPAsync(dto.UserNRP);
        if (user is null) throw new Exception("User tidak ditemukan");

        var project = await _projectRepository.GetByCodeAsync(dto.ProjectCode);
        if (project is null) throw new Exception("Project tidak ditemukan");

        var member = new ProjectMember
        {
            ProjectId = project.Id,
            UserId = user.Id,
            JoinedAt = DateTime.UtcNow
        };

        await _memberRepository.AddAsync(member);
        await _memberRepository.SaveChangesAsync();

        await _notificationService.NotifyAddedToProject(project, new List<User> { user });

        return Unit.Value;
    }
}
