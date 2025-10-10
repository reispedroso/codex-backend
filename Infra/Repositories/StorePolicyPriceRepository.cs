using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Database;
using codex_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace codex_backend.Infra.Repositories;

public class StorePolicyPriceRepository(AppDbContext context) : IStorePolicyPricesRepository
{
    private readonly AppDbContext _context = context;

    public async Task<StorePolicyPrice> CreateStorePolicyPricesAsync(StorePolicyPrice storePolicyPrice)
    {
        await _context.StorePolicyPrice.AddAsync(storePolicyPrice);
        await _context.SaveChangesAsync();
        return storePolicyPrice;
    }

    public async Task<StorePolicyPrice?> GetPolicyPriceByIdAsync(Guid priceId)
    {
        return await _context.StorePolicyPrice.FirstOrDefaultAsync(spp => spp.Id == priceId);
    }

    public async Task<bool> UpdateStorePolicyPricesAsync(StorePolicyPrice storePolicyPrice)
    {
        _context.Update(storePolicyPrice);
        var updated = await _context.SaveChangesAsync();
        return updated > 0;
    }

        public async Task<StorePolicyPrice?> GetPriceByPolicyAndDurationAsync(Guid policyId, int durationInMonths)
    {
        return await _context.StorePolicyPrice
            .FirstOrDefaultAsync(p => p.StorePolicyId == policyId && p.DurationInMonths == durationInMonths);
    }
}