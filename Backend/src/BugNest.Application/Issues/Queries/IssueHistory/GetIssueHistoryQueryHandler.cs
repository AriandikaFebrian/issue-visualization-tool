using BugNest.Application.DTOs.Issues;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Queries;

public class GetIssueHistoryQueryHandler : IRequestHandler<GetIssueHistoryQuery, List<IssueHistoryDto>>
{
    private readonly IIssueHistoryRepository _historyRepository;
    private readonly IIssueRepository _issueRepository;

    public GetIssueHistoryQueryHandler(
        IIssueHistoryRepository historyRepository,
        IIssueRepository issueRepository)
    {
        _historyRepository = historyRepository;
        _issueRepository = issueRepository;
    }

    public async Task<List<IssueHistoryDto>> Handle(GetIssueHistoryQuery request, CancellationToken cancellationToken)
    {
        var issue = await _issueRepository.GetByCodeAsync(request.IssueCode);
        if (issue == null)
            throw new Exception("Issue not found");

        var historyList = await _historyRepository.GetByIssueIdAsync(issue.Id);

        return historyList.Select(h => new IssueHistoryDto
        {
            ChangeType = h.ChangeType.ToString(),
            PreviousValue = h.PreviousValue,
            NewValue = h.NewValue,
            Note = h.Note,
            ChangedBy = h.ChangedBy,
            ChangedByUsername = h.ChangedByUsername,
            ChangedByProfileUrl = h.ChangedByProfileUrl,
            ChangedFromIP = h.ChangedFromIP,
            SourcePlatform = h.SourcePlatform,
            CreatedAt = h.CreatedAt
        }).ToList();
    }
}
