using BugNest.Application.DTOs.Projects;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetProjectDetail;

public class GetProjectDetailQuery : IRequest<ProjectDetailDto?>
{
    public string ProjectCode { get; }
    public string UserNRP { get; }

    public GetProjectDetailQuery(string projectCode, string userNrp)
    {
        ProjectCode = projectCode;
        UserNRP = userNrp;
    }
}
