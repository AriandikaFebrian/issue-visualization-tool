public class CreateIssueDto
{
    public string ProjectCode { get; set; } = string.Empty;

    // 📝 Informasi utama
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string StepsToReproduce { get; set; } = string.Empty;
    public string DeviceInfo { get; set; } = string.Empty;

    // ⚙️ Metadata
    public IssueType Type { get; set; } = IssueType.Bug;
    public PriorityLevel Priority { get; set; }

    // ⏳ Estimasi
    public DateTime? Deadline { get; set; }
    public int? EstimatedFixHours { get; set; }

    // 👤 Audit & assignment
    public Guid CreatedBy { get; set; }
    public string? CreatedByNRP { get; set; }
    public List<string>? AssignedUserNRPs { get; set; }

    // 🏷️ Tagging
    public List<Guid>? TagIds { get; set; }

}
