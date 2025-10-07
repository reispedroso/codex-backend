using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IStorePolicyPricesRepository
{
    Task<StorePolicyPrice> CreateStorePolicyPricesAsync(StorePolicyPrice storePolicyPrice);
    Task<StorePolicyPrice> GetPolicyPriceByIdAsync(Guid policeId);
    Task<bool> UpdateStorePolicyPricesAsync( StorePolicyPrice storePolicyPrice);
}
