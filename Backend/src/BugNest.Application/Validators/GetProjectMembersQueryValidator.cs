using FluentValidation;

namespace BugNest.Application.Projects.Queries.GetProjectMembers;

public class GetProjectMembersQueryValidator : AbstractValidator<GetProjectMembersQuery>
{
    public GetProjectMembersQueryValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty().WithMessage("ProjectId tidak boleh kosong.");
    }
}
