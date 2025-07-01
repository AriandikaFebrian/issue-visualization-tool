using MediatR;
using BugNest.Application.DTOs.Issues;
using System.Collections.Generic;

namespace BugNest.Application.UseCases.Issues.Queries
{
    public class GetRecentIssuesQuery : IRequest<List<RecentIssueDto>>
    {
        public int Count { get; }

        public GetRecentIssuesQuery(int count = 5)
        {
            Count = count;
        }
    }
}
