using BugNest.Application.DTOs.Issues;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Queries;

public class GetIssueHistoryQuery : IRequest<List<IssueHistoryDto>>
{
    public string IssueCode { get; }

    public GetIssueHistoryQuery(string issueCode)
    {
        IssueCode = issueCode;
    }
}
