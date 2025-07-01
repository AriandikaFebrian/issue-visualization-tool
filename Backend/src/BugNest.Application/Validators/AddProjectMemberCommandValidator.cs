using FluentValidation;

namespace BugNest.Application.Projects.Commands.AddProjectMember;

public class AddProjectMemberCommandValidator : AbstractValidator<AddProjectMemberCommand>
{
    public AddProjectMemberCommandValidator()
    {
        RuleFor(x => x.Dto.UserNRP)
            .NotEmpty().WithMessage("NRP pengguna tidak boleh kosong.");

        RuleFor(x => x.Dto.ProjectCode)
            .NotEmpty().WithMessage("Kode proyek tidak boleh kosong.");
    }
}
