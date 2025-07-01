using BugNest.Application.DTOs.ActivityLogs;

public class GetActivityLogsResult
{
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public List<ActivityLogDto> Data { get; set; } = new();
}
