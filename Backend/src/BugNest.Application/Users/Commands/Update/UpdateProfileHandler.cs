using MediatR;
using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;

namespace BugNest.Application.Users.Commands;

public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand, User?>
{
    private readonly IUserRepository _userRepository;

    public UpdateProfileHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null)
            return null;

        var request = command.Request;

        if (!string.IsNullOrWhiteSpace(request.Username) && request.Username != user.Username)
        {
            var usernameExists = await _userRepository.UsernameExistsAsync(request.Username, command.UserId);
            if (usernameExists)
                throw new InvalidOperationException("Username already taken");

            user.Username = request.Username;
        }

        user.FullName = request.FullName ?? user.FullName;
        user.ProfilePictureUrl = request.ProfilePictureUrl ?? user.ProfilePictureUrl;
        user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
        user.Department = request.Department ?? user.Department;
        user.Position = request.Position ?? user.Position;

        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        return user;
    }
}
