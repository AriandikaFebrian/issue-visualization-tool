using FluentValidation;

namespace BugNest.Application.Projects.Queries.GetProjectsByOwner;

public class GetProjectsByOwnerQueryValidator : AbstractValidator<GetProjectsByOwnerQuery>
{
    public GetProjectsByOwnerQueryValidator()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty().WithMessage("Owner ID tidak boleh kosong.");
    }
}
