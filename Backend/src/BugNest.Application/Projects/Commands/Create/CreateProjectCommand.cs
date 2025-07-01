using BugNest.Application.Common.Dtos;
using BugNest.Domain.Enums;
using MediatR;

namespace BugNest.Application.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<Guid>, IAuditableCommand
{
    public CreateProjectDto Dto { get; }

    // Properti tambahan untuk logging
    public Guid CreatedProjectId { get; set; } // ← akan diisi di handler
    public string NRP { get; set; }            // ← dari controller/user context

    public CreateProjectCommand(CreateProjectDto dto, string nrp)
    {
        Dto = dto;
        NRP = nrp;
    }

    // IAuditableCommand Implementation
    public ActivityAction Action => ActivityAction.CreatedProject;
    public ActivityEntityType TargetEntityType => ActivityEntityType.Project;
    public Guid TargetEntityId => CreatedProjectId;
    public string Summary => $"User {NRP} created project '{Dto.Name}'";
    public string? PerformedByNRP => NRP;

   public Guid ProjectId 
{ 
    get => CreatedProjectId; 
    set => CreatedProjectId = value; 
}
}
