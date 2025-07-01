using BugNest.Application.DTOs.Projects;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetProjectSummary;

public class GetProjectSummaryQuery : IRequest<ProjectSummaryDto?>
{
    public string ProjectCode { get; }

    public GetProjectSummaryQuery(string projectCode)
    {
        ProjectCode = projectCode;
    }
}
