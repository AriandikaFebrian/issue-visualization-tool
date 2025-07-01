namespace BugNest.Application.DTOs.Projects;

public class UploadProjectSourceDto
{
    public Guid ProjectId { get; set; }

    // Nama file ZIP yang diunggah (optional untuk validasi/preview)
    public string? FileName { get; set; }
}
