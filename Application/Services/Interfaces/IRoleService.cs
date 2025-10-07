namespace codex_backend.Application.Services.Interfaces;

public interface IRoleService
{
    Task<string?> GetRoleNameAsync(Guid id);
    Task<Guid> GetRoleIdAsync(string name);
}