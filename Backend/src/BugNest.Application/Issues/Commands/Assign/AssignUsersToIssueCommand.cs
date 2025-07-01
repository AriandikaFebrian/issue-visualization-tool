using MediatR;

namespace BugNest.Application.UseCases.Issues.Commands;

public class AssignUsersToIssueCommand : IRequest<Unit>
{
    public string IssueCode { get; }
    public List<string> NRPs { get; }

    public AssignUsersToIssueCommand(string issueCode, List<string> nrps)
    {
        IssueCode = issueCode;
        NRPs = nrps;
    }
}
