using FluentValidation;

namespace BugNest.Application.Projects.Queries.GetProjectDetail;

public class GetProjectDetailQueryValidator : AbstractValidator<GetProjectDetailQuery>
{
    public GetProjectDetailQueryValidator()
    {
        RuleFor(x => x.ProjectCode)
            .NotEmpty().WithMessage("Kode proyek tidak boleh kosong.");
    }
}
