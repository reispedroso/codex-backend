namespace codex_backend.Application.Dtos;

public class UserLoginDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class UserResponseDto
{
    public string? Token { get; set; }
    public string? Email { get; set; }
    public DateTime Expiration { get; set; }
}