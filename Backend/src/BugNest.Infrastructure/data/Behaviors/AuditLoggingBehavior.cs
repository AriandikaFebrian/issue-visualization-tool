using MediatR;
using BugNest.Application.Interfaces;
using BugNest.Domain.Entities;
using BugNest.Domain.Enums;
using Microsoft.AspNetCore.Http;

public class AuditLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IActivityLogRepository _logRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContext;

    public AuditLoggingBehavior(
        IActivityLogRepository logRepository,
        IUserRepository userRepository,
        IHttpContextAccessor httpContext)
    {
        _logRepository = logRepository;
        _userRepository = userRepository;
        _httpContext = httpContext;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
{
    var response = await next();

    if (request is IAuditableCommand auditable)
    {
        // Debug nilai auditable
        Console.WriteLine($"Audit Log - PerformedByNRP: {auditable.PerformedByNRP}");
        Console.WriteLine($"Audit Log - ProjectId: {auditable.ProjectId}");
        Console.WriteLine($"Audit Log - TargetEntityId: {auditable.TargetEntityId}");
        Console.WriteLine($"Audit Log - Action: {auditable.Action}");
        Console.WriteLine($"Audit Log - TargetEntityType: {auditable.TargetEntityType}");
        Console.WriteLine($"Audit Log - Summary: {auditable.Summary}");

        var user = await _userRepository.GetByNRPAsync(auditable.PerformedByNRP ?? "");
        if (user == null)
        {
            Console.WriteLine("Audit Log - User not found by NRP");
        }
        else
        {
            var log = new ActivityLog
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                ProjectId = auditable.ProjectId,
                Action = auditable.Action,
                TargetEntityId = auditable.TargetEntityId,
                TargetEntityType = auditable.TargetEntityType,
                Summary = auditable.Summary,
                CreatedAt = DateTime.UtcNow,
                SourcePlatform = _httpContext.HttpContext?.Request.Headers["X-Platform"],
                IPAddress = _httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString()
            };

            await _logRepository.AddAsync(log);
            await _logRepository.SaveChangesAsync();

            Console.WriteLine("Audit Log - Log saved to repository");
        }
    }

    return response;
}

}
