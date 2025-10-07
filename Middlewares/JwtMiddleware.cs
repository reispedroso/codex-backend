using codex_backend.Application.Services.Token;
using Microsoft.AspNetCore.Authorization;

namespace codex_backend.Middlewares;

public class JwtMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, IJwtService jwtService)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata?.GetMetadata<IAuthorizeData>() == null)
        {
            await _next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var claimsPrincipal = jwtService.ValidateToken(token);
            if (claimsPrincipal != null)
            {
                context.User = claimsPrincipal;
            }
        }

        await _next(context);
    }
}