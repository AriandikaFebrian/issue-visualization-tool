using MediatR;
using BugNest.Application.DTOs.ActivityLogs;

public class GetActivityLogDetailByIdQuery : IRequest<ActivityLogDetailDto>
{
    public Guid Id { get; }

    public GetActivityLogDetailByIdQuery(Guid id)
    {
        Id = id;
    }
}
