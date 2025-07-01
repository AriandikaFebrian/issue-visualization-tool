using BugNest.Application.DTOs.Issues;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Queries;

public class GetAssignedIssuesQuery : IRequest<List<IssueDto>>
{
    public Guid UserId { get; }

    public GetAssignedIssuesQuery(Guid userId)
    {
        UserId = userId;
    }
}
