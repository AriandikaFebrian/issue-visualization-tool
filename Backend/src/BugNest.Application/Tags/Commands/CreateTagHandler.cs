using BugNest.Application.DTOs.Tags;
using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using MediatR;

namespace BugNest.Application.UseCases.Tags.Commands;

public class CreateTagHandler : IRequestHandler<CreateTagCommand, Guid>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IUserContext _userContext; // ⬅️ Tambahkan ini

    public CreateTagHandler(
        ITagRepository tagRepository,
        IUserRepository userRepository,
        IProjectRepository projectRepository,
        IUserContext userContext) // ⬅️ Inject juga di sini
    {
        _tagRepository = tagRepository;
        _userRepository = userRepository;
        _projectRepository = projectRepository;
        _userContext = userContext;
    }

    public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;

        var nrp = _userContext.GetNRP();
        if (string.IsNullOrWhiteSpace(nrp))
            throw new Exception("NRP tidak ditemukan di konteks user.");

        var user = await _userRepository.GetByNRPAsync(nrp);
        if (user == null)
            throw new Exception("User tidak ditemukan");

        if (!string.IsNullOrEmpty(dto.ProjectCode))
        {
            var project = await _projectRepository.GetByCodeAsync(dto.ProjectCode);
            if (project == null)
                throw new Exception("Project tidak ditemukan");
        }

        var tag = new Tag
        {
            Name = dto.Name,
            Color = dto.Color,
            Category = dto.Category,
            ProjectCode = dto.ProjectCode,
            CreatedBy = user.Id,
            Creator = user,
            CreatedAt = DateTime.UtcNow,
            IsArchived = false
        };

        await _tagRepository.AddAsync(tag);
        await _tagRepository.SaveChangesAsync();

        return tag.Id;
    }
}

