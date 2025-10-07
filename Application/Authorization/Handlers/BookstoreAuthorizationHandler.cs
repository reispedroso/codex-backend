using System.Security.Claims;
using codex_backend.Application.Authorization.Requirements;
using codex_backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace codex_backend.Application.Authorization.Handlers;

public class BookstoreAuthorizationHandler 
    : AuthorizationHandler<ResourceOwnerRequirement, Bookstore>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ResourceOwnerRequirement requirement,
        Bookstore resource)
    {
        if (context.User.IsInRole("Admin"))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is not null && resource.OwnerUserId == Guid.Parse(userId))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}