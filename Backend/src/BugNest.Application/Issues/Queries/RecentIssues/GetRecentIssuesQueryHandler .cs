using BugNest.Application.DTOs.Issues;
using BugNest.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BugNest.Application.UseCases.Issues.Queries
{
    public class GetRecentIssuesQueryHandler : IRequestHandler<GetRecentIssuesQuery, List<RecentIssueDto>>
    {
        private readonly IIssueRepository _issueRepository;

        public GetRecentIssuesQueryHandler(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public async Task<List<RecentIssueDto>> Handle(GetRecentIssuesQuery request, CancellationToken cancellationToken)
        {
            var recentIssues = await _issueRepository.GetRecentIssuesAsync(request.Count);

            return recentIssues.Select(issue => new RecentIssueDto
            {
                IssueCode = issue.IssueCode,
                Title = issue.Title,
                ProjectCode = issue.Project?.ProjectCode ?? "-",
                Status = issue.Status.ToString(),
                CreatedAt = issue.CreatedAt
            }).ToList();
        }
    }
}
