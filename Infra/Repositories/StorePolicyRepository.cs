using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class StorePolicyRepository(AppDbContext context) : IStorePolicyRepository
{
    private readonly AppDbContext _context = context;

    public async Task<StorePolicy> CreateStorePolicyAsync(StorePolicy storePolicy)
    {
        await _context.StorePolicy.AddAsync(storePolicy);
        await _context.SaveChangesAsync();
        return storePolicy;
    }
    public async Task<StorePolicy?> GetPolicyByIdAsync(Guid policyId)
    {
        return await _context.StorePolicy.FirstOrDefaultAsync(sp => sp.Id == policyId);
    }

    public async Task<bool> UpdateStorePolicyAsync(StorePolicy storePolicy)
    {
        _context.Update(storePolicy);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }
}