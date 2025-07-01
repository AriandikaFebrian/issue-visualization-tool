using BugNest.Domain.Enums;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Commands;

public class ChangeIssueStatusCommand : IRequest<Unit>, IAuditableCommand
{
    public string IssueCode { get; set; }
    public IssueStatus NewStatus { get; set; }
    public string ChangedByNRP { get; set; }
    public string? Note { get; set; }

    // Akan diisi oleh handler
    public Guid ChangedIssueId { get; set; }
    public Guid ProjectId { get; set; }

    // ✅ Untuk keperluan audit log (optional tapi berguna)
    public string? PreviousValue { get; set; }
    public string? NewValue { get; set; }

    public ChangeIssueStatusCommand(string issueCode, IssueStatus newStatus, string changedByNRP, string? note = null)
    {
        IssueCode = issueCode;
        NewStatus = newStatus;
        ChangedByNRP = changedByNRP;
        Note = note;
    }

    public ActivityAction Action => ActivityAction.ChangedIssueStatus;
    public ActivityEntityType TargetEntityType => ActivityEntityType.Issue;
    public Guid TargetEntityId => ChangedIssueId;
    public string Summary => $"User {ChangedByNRP} changed status of issue '{IssueCode}' from {PreviousValue ?? "-"} to {NewValue ?? NewStatus.ToString()}";
    public string? PerformedByNRP => ChangedByNRP;
}
