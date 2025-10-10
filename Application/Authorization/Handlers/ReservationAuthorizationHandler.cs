using System.Security.Claims;
using codex_backend.Application.Authorization.Requirements;
using codex_backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace codex_backend.Application.Authorization.Handlers;

public class ReservationAuthorizationHandler : AuthorizationHandler<ResourceOwnerRequirement, Reservation>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwnerRequirement requirement,
        Reservation resource
        )
    {
        var loggedUserId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (loggedUserId == null)
        {
            return Task.CompletedTask;
        }

        if (resource.UserId == Guid.Parse(loggedUserId))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        return Task.CompletedTask;
    }
}