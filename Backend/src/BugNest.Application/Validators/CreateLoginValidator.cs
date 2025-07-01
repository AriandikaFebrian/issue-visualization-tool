using BugNest.Application.Users.Commands;
using FluentValidation;

namespace BugNest.Application.Validators;

public class CreateLoginValidator : AbstractValidator<LoginCommand>
{
    public CreateLoginValidator()
    {
        RuleFor(x => x.Identifier)
            .NotEmpty().WithMessage("Email atau NRP wajib diisi.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password wajib diisi.");
    }
}
