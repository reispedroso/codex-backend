namespace codex_backend.Application.Authorization.Wrappers;

public class ApiResponse(bool success, string message)
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
}

public class ApiSingleResponse<T>(bool success, string message, T? data) : ApiResponse(success, message)
{
    public T? Data { get; set; } = data;
}

public class ApiListResponse<T>(bool success, string message, IEnumerable<T> data) : ApiResponse(success, message)
{
    public IEnumerable<T> Data { get; set; } = data;
}