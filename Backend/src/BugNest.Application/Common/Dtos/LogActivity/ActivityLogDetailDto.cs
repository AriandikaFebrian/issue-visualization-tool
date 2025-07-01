using BugNest.Application.DTOs.ActivityLogs;

public class ActivityLogDetailDto : ActivityLogDto
{
    public string? PreviousValue { get; set; }
    public string? NewValue { get; set; }
    public string? Note { get; set; }

    public string? TargetEntityCode { get; set; }
    public string? TargetEntityName { get; set; }
}
