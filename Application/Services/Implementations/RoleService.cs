using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Interfaces;

namespace codex_backend.Application.Services.Implementations;

public class RoleService(IRoleRepository repository) 
: IRoleService
{
    private readonly IRoleRepository _repository = repository;

    public async Task<Guid> GetRoleIdAsync(string name)
    {
        return await _repository.GetRoleIdAsync(name);
    }

    public async Task<string?> GetRoleNameAsync(Guid Id)
    {
        return await _repository.GetRoleNameAsync(Id)
        ?? throw new Exception("Role not found");
    }

    
}