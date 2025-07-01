using BugNest.Application.Common.Dtos;
using BugNest.Domain.Enums;
using MediatR;

namespace BugNest.Application.Projects.Commands.CreateProject;

public class CreateProjectCommand : IRequest<Guid>, IAuditableCommand
{
    public CreateProjectDto Dto { get; }
    public Guid CreatedProjectId { get; set; }
    public string NRP { get; set; }

    public CreateProjectCommand(CreateProjectDto dto, string nrp)
    {
        Dto = dto;
        NRP = nrp;
    }
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
