using BugNest.Application.DTOs.Projects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BugNest.Application.UseCases.Projects.Commands;

public class UploadProjectSourceCommand : IRequest<bool>
{
    public UploadProjectSourceDto Dto { get; set; }

    // Ini akan diisi langsung dari Controller pakai IFormFile
    public IFormFile File { get; set; }

    public UploadProjectSourceCommand(UploadProjectSourceDto dto, IFormFile file)
    {
        Dto = dto;
        File = file;
    }
}
