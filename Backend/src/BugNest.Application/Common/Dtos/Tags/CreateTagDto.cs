namespace BugNest.Application.DTOs.Tags;

public class CreateTagDto
{
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#cccccc";
    public string? ProjectCode { get; set; }
    public string CreatedByNRP { get; set; } = string.Empty;
    public string? Category { get; set; }
}
