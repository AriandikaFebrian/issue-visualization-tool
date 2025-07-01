using MediatR;
using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Domain.Enums;
using BugNest.Application.Common.Dtos;

namespace BugNest.Application.Users.Commands;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public RegisterUserHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<RegisterUserResponseDto?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (await _userRepository.EmailExistsAsync(dto.Email))
            return new RegisterUserResponseDto { Message = "Email sudah terdaftar." };

        if (await _userRepository.UsernameExistsAsync(dto.Username))
            return new RegisterUserResponseDto { Message = "Username sudah digunakan." };
        DepartmentType? department = Enum.TryParse<DepartmentType>(dto.Department, true, out var dept) ? dept : null;
        PositionType? position = Enum.TryParse<PositionType>(dto.Position, true, out var pos) ? pos : null;

        var role = Enum.TryParse<UserRole>(dto.Role, true, out var parsedRole) ? parsedRole : UserRole.Developer;
        var generatedNrp = await GenerateNRPAsync(role);
        var newUser = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = _authService.HashPassword(dto.Password),
            Role = role,
            NRP = generatedNrp,
            FullName = dto.FullName,
            ProfilePictureUrl = dto.ProfilePictureUrl,
            PhoneNumber = dto.PhoneNumber,
            Department = department,
            Position = position,
            IsActive = true,
            RegisteredAt = DateTime.UtcNow,
            LastLogin = DateTime.UtcNow
        };

        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveChangesAsync();

        return new RegisterUserResponseDto
        {
            NRP = newUser.NRP,
            Message = "Register berhasil"
        };
    }

    private string GetPrefixForRole(UserRole role)
    {
        return role switch
        {
            UserRole.ProjectManager => "PM",
            UserRole.TechLead => "TL",
            UserRole.Developer => "DEV",
            UserRole.QA => "QA",
            _ => "USR"
        };
    }

    private async Task<string> GenerateNRPAsync(UserRole role)
    {
        var year = DateTime.UtcNow.Year;
        int urutan = 1;
        string prefix = GetPrefixForRole(role);
        string nrp;

        do
        {
            nrp = $"{prefix}{year}{urutan:D4}";
            urutan++;
        } while (await _userRepository.NRPExistsAsync(nrp));

        return nrp;
    }
}
