// 📁 Application/UseCases/Issues/Queries/GetIssuesByProjectQuery.cs
using BugNest.Application.DTOs.Issues;
using MediatR;

namespace BugNest.Application.UseCases.Issues.Queries;

public class GetIssuesByProjectQuery : IRequest<List<IssueDto>>
{
    public string ProjectCode { get; }

    public GetIssuesByProjectQuery(string projectCode)
    {
        ProjectCode = projectCode;
    }
}
