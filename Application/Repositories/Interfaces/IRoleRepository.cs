namespace codex_backend.Application.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<string> GetRoleNameAsync(Guid id);
    Task<Guid> GetRoleIdAsync(string name);

}