using BugNest.Application.DTOs.ActivityLogs;
using MediatR;

using MediatR;

public class GetActivityLogsQuery : IRequest<GetActivityLogsResult>
{
    public int Page { get; }
    public int PageSize { get; }

    public GetActivityLogsQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}


