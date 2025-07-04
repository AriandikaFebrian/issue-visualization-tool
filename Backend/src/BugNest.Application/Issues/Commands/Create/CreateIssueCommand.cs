using BugNest.Application.DTOs.Issues;
using BugNest.Domain.Enums;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Commands;

public class CreateIssueCommand : IRequest<IssueCreatedDto>, IAuditableCommand
{
    public CreateIssueDto Dto { get; }
    public string NRP { get; }
    public Guid CreatedIssueId { get; set; } = Guid.Empty;
   public Guid CreatedProjectId { get; set; }
    public CreateIssueCommand(CreateIssueDto dto, string nrp)
    {
        Dto = dto;
        NRP = nrp;
    }
    public ActivityAction Action => ActivityAction.CreatedIssue;
    public ActivityEntityType TargetEntityType => ActivityEntityType.Issue;
    public Guid TargetEntityId => CreatedIssueId;
    public string Summary => $"User {NRP} created issue '{Dto.Title}'";
    public string? PerformedByNRP => NRP;
    public Guid ProjectId
    {
        get => CreatedProjectId;
        set => CreatedProjectId = value;
    }
}
