using FluentValidation;

namespace BugNest.Application.Projects.Queries.GetProjectSummary;

public class GetProjectSummaryQueryValidator : AbstractValidator<GetProjectSummaryQuery>
{
    public GetProjectSummaryQueryValidator()
    {
        RuleFor(x => x.ProjectCode)
            .NotEmpty().WithMessage("Project code tidak boleh kosong.");
    }
}
