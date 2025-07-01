using BugNest.Application.Projects.Commands.CreateProject;
using FluentValidation;
using BugNest.Domain.Enums;

namespace BugNest.Application.Projects.Validators;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage("Nama proyek wajib diisi.")
            .MaximumLength(100).WithMessage("Nama proyek maksimal 100 karakter.");

        RuleFor(x => x.Dto.Description)
            .MaximumLength(500).WithMessage("Deskripsi maksimal 500 karakter.");

        RuleFor(x => x.Dto.ProjectCode)
            .MaximumLength(30).WithMessage("Kode proyek maksimal 30 karakter.")
            .Matches("^[a-zA-Z0-9\\-]*$").WithMessage("Kode proyek hanya boleh mengandung huruf, angka, dan tanda '-'.");

        RuleFor(x => x.Dto.RepositoryUrl)
            .MaximumLength(200).WithMessage("URL repository terlalu panjang.")
            .When(x => !string.IsNullOrWhiteSpace(x.Dto.RepositoryUrl));

        RuleFor(x => x.Dto.DocumentationUrl)
            .MaximumLength(200).WithMessage("URL dokumentasi terlalu panjang.")
            .When(x => !string.IsNullOrWhiteSpace(x.Dto.DocumentationUrl));

        RuleFor(x => x.Dto.Status)
            .IsInEnum()
            .When(x => x.Dto.Status.HasValue)
            .WithMessage("Status proyek tidak valid.");

        RuleFor(x => x.Dto.Visibility)
            .IsInEnum()
            .WithMessage("Tipe visibilitas tidak valid.");
    }
}
