namespace codex_backend.Application.Dtos;
public class TokenResultDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
}