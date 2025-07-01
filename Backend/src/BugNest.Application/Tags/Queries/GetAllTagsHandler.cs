using MediatR;
using BugNest.Application.DTOs.Tags;
using System.Collections.Generic;
using BugNest.Application.DTOs.Issues;

namespace BugNest.Application.UseCases.Tags.Queries
{
    public class GetAllTagsQuery : IRequest<List<TagDto>>
    {
        public string? ProjectCode { get; set; }

        public GetAllTagsQuery(string? projectCode = null)
        {
            ProjectCode = projectCode;
        }
    }
}
