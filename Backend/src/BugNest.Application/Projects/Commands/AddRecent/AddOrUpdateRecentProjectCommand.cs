// 📁 Application/Projects/Commands/AddOrUpdateRecentProject/AddOrUpdateRecentProjectCommand.cs
using MediatR;

namespace BugNest.Application.Projects.Commands.AddOrUpdateRecentProject;
public class AddOrUpdateRecentProjectCommand : IRequest<Unit> 
{
    public string UserNRP { get; }
    public string ProjectCode { get; }

    public AddOrUpdateRecentProjectCommand(string userNRP, string projectCode)
    {
        UserNRP = userNRP;
        ProjectCode = projectCode;
    }
}
