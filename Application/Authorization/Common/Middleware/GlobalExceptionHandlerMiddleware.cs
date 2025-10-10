using System.Net;
using System.Text.Json;
using codex_backend.Application.Authorization.Common.Exceptions;

namespace codex_backend.Application.Authorization.Common.Middleware;

public class GlobalExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var statusCode = exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            DuplicateException => HttpStatusCode.Conflict,

            _ => HttpStatusCode.InternalServerError,
        };

        response.StatusCode = (int)statusCode;
        var result = JsonSerializer.Serialize(new { error = exception.Message });
        return response.WriteAsync(result);
    }
}