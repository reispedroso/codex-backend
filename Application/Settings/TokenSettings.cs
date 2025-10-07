namespace codex_backend.Application.Settings;

public class TokenSettings
{
    public string Key { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public int ExpiresInDays { get; init; }
}