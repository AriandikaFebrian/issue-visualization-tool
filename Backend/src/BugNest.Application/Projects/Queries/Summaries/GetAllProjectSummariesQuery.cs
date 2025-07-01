using BugNest.Application.DTOs.Projects;
using MediatR;

namespace BugNest.Application.Projects.Queries.GetAllProjectSummaries;

public class GetAllProjectSummariesQuery : IRequest<List<ProjectSummaryDto>>
{
}

