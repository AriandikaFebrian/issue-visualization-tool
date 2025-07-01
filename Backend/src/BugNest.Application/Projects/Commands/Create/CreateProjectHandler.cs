// 📁 Application/Projects/Commands/CreateProject/CreateProjectHandler.cs
using BugNest.Application.DTOs.Projects;
using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Domain.Enums;
using MediatR;

namespace BugNest.Application.Projects.Commands.CreateProject;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserContext _userContext;

    public CreateProjectHandler(
        IProjectRepository projectRepository,
        IUserRepository userRepository,
        IUserContext userContext)
    {
        _projectRepository = projectRepository;
        _userRepository = userRepository;
        _userContext = userContext;
    }

    public async Task<Guid> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
{
    var dto = request.Dto;

    var ownerNrp = _userContext.GetNRP();
    if (string.IsNullOrWhiteSpace(ownerNrp))
        throw new Exception("Tidak dapat mengambil NRP user dari konteks.");

    var ownerUser = await _userRepository.GetByNRPAsync(ownerNrp);
    if (ownerUser == null)
        throw new Exception("User dengan NRP tersebut tidak ditemukan.");

    var project = new Project
    {
        Name = dto.Name,
        Description = dto.Description,
        ProjectCode = string.IsNullOrWhiteSpace(dto.ProjectCode)
            ? GenerateProjectCode()
            : dto.ProjectCode,
        RepositoryUrl = dto.RepositoryUrl,
        DocumentationUrl = dto.DocumentationUrl,
        Status = dto.Status ?? ProjectStatus.Planning,
        Visibility = dto.Visibility,
        OwnerId = ownerUser.Id,
        CreatedAt = DateTime.UtcNow
    };

    await _projectRepository.AddAsync(project);
    await _projectRepository.SaveChangesAsync();

    // ✅ Tambahkan ini agar pipeline logging bisa bekerja
    request.CreatedProjectId = project.Id;

    return project.Id;
}


    private string GenerateProjectCode()
    {
        var random = Guid.NewGuid().ToString("N")[..6].ToUpper();
        return $"PRJ-{random}";
    }

}
