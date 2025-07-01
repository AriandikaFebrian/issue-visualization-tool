using BugNest.Application.DTOs.Tags;
using MediatR;

namespace BugNest.Application.UseCases.Tags.Commands;

public class CreateTagCommand : IRequest<Guid>
{
    public CreateTagDto Dto { get; }

    public CreateTagCommand(CreateTagDto dto)
    {
        Dto = dto;
    }
}
