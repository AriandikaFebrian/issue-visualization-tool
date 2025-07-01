using FluentValidation;
using BugNest.Application.Common.Dtos;

namespace BugNest.Application.Validators;
public class CreateRegisterValidator : AbstractValidator<RegisterUserDto>
{
    public CreateRegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username tidak boleh kosong.")
            .MinimumLength(3).WithMessage("Username minimal 3 karakter.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email tidak boleh kosong.")
            .EmailAddress().WithMessage("Format email tidak valid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password tidak boleh kosong.")
            .MinimumLength(6).WithMessage("Password minimal 6 karakter.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role harus diisi.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Nama lengkap harus diisi.");
    }
}
