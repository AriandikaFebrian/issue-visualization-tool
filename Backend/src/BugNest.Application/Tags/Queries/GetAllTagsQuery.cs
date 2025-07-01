using BugNest.Application.DTOs.Issues;
using BugNest.Application.DTOs.Tags;
using BugNest.Application.Interfaces;
using MediatR;

namespace BugNest.Application.UseCases.Tags.Queries;

public class GetAllTagsHandler : IRequestHandler<GetAllTagsQuery, List<TagDto>>
{
    private readonly ITagRepository _tagRepository;

    public GetAllTagsHandler(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<List<TagDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetAllAsync();

        if (!string.IsNullOrEmpty(request.ProjectCode))
        {
            tags = tags.Where(t => t.ProjectCode == request.ProjectCode).ToList();
        }

        return tags.Select(t => new TagDto
        {
            Id = t.Id,
            Name = t.Name,
            Color = t.Color,
            Category = t.Category,
            ProjectCode = t.ProjectCode
        }).ToList();
    }
}
