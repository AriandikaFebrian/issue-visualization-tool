using BugNest.Application.DTOs.Issues;
using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Domain.Enums;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Commands;

public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, IssueCreatedDto>
{
    private readonly IIssueRepository _issueRepository;
    private readonly IProjectMemberRepository _projectMemberRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;

    public CreateIssueCommandHandler(
        IIssueRepository issueRepository,
        IProjectMemberRepository projectMemberRepository,
        IUserRepository userRepository,
        IProjectRepository projectRepository)
    {
        _issueRepository = issueRepository;
        _projectMemberRepository = projectMemberRepository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
    }

    public async Task<IssueCreatedDto> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
{
    var dto = request.Dto;

    var project = await _projectRepository.GetByCodeAsync(dto.ProjectCode);
    if (project == null)
        throw new Exception("Project dengan kode tersebut tidak ditemukan.");

    if (!string.IsNullOrEmpty(dto.CreatedByNRP))
    {
        var creator = await _userRepository.GetByNRPAsync(dto.CreatedByNRP);
        if (creator is null)
            throw new Exception("User dengan NRP tersebut tidak ditemukan.");
        dto.CreatedBy = creator.Id;
    }

    var isMember = await _projectMemberRepository.IsUserInProject(dto.CreatedBy, project.Id);
    if (!isMember)
        throw new UnauthorizedAccessException("You are not a member of this project.");

    var issue = new Issue
    {
        ProjectId = project.Id,
        IssueCode = GenerateIssueCode(project.ProjectCode),
        Title = dto.Title,
        Description = dto.Description,
        StepsToReproduce = dto.StepsToReproduce,
        DeviceInfo = dto.DeviceInfo,
        Priority = dto.Priority,
        Status = IssueStatus.Open,
        Deadline = dto.Deadline,
        EstimatedFixHours = dto.EstimatedFixHours,
        CreatedBy = dto.CreatedBy,
        CreatedAt = DateTime.UtcNow,
        Type = dto.Type,
    };

    await _issueRepository.AddAsync(issue);

    if (dto.TagIds is not null && dto.TagIds.Any())
    {
        var tags = dto.TagIds.Select(tagId => new IssueTag
        {
            IssueId = issue.Id,
            TagId = tagId
        }).ToList();

        await _issueRepository.AddTagsAsync(tags);
    }

    if (dto.AssignedUserNRPs is not null && dto.AssignedUserNRPs.Any())
    {
        var assignedUsers = new List<AssignedUser>();
        foreach (var nrp in dto.AssignedUserNRPs)
        {
            var user = await _userRepository.GetByNRPAsync(nrp);
            if (user != null)
            {
                assignedUsers.Add(new AssignedUser
                {
                    IssueId = issue.Id,
                    UserId = user.Id
                });
            }
        }
        await _issueRepository.AddAssignedUsersAsync(assignedUsers);
    }

    await _issueRepository.SaveChangesAsync();
    request.CreatedIssueId = issue.Id;
    request.ProjectId = project.Id;

    return new IssueCreatedDto
    {
        IssueId = issue.Id,
        IssueCode = issue.IssueCode
    };
}


    private string GenerateIssueCode(string projectCode)
    {
        var unique = Guid.NewGuid().ToString("N")[..6].ToUpper();
        return $"{projectCode}-ISSUE-{unique}";
    }
}
