public class CreateIssueDto
{
    public string ProjectCode { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StepsToReproduce { get; set; } = string.Empty;
    public string DeviceInfo { get; set; } = string.Empty;
    public IssueType Type { get; set; } = IssueType.Bug;
    public PriorityLevel Priority { get; set; }
    public DateTime? Deadline { get; set; }
    public int? EstimatedFixHours { get; set; }
    public Guid CreatedBy { get; set; }
    public string? CreatedByNRP { get; set; }
    public List<string>? AssignedUserNRPs { get; set; }
    public List<Guid>? TagIds { get; set; }

}
