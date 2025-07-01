using MediatR;

namespace Application.Projects.Queries.GetProjectIssueDetail;

public class GetProjectIssueDetailQuery : IRequest<ProjectIssueDetailDto>
{
    public string ProjectCode { get; set; }

    public GetProjectIssueDetailQuery(string projectCode)
    {
        ProjectCode = projectCode;
    }
}
