using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class RoleRepository(AppDbContext context) : IRoleRepository
{
    private readonly AppDbContext _context = context;
    public async Task<string?> GetRoleNameAsync(Guid id)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
        if (role is null)
            return null;
        else
            return role.Name;
    }

    public async Task<Guid> GetRoleIdAsync(string name)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        return role.Id;
    }


}