namespace BugNest.Application.Common.Dtos;
public class UpdateProfileRequestDto
{
    public string? Username { get; set; }
    public string? FullName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? PhoneNumber { get; set; }
    public DepartmentType? Department { get; set; }
    public PositionType? Position { get; set; }
}
