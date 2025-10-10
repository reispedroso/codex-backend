using codex_backend.Models;

namespace codex_backend.Application.Repositories.Interfaces;

public interface IStorePolicyRepository
{
    Task<StorePolicy> CreateStorePolicyAsync(StorePolicy storePolicy);
    Task<bool> UpdateStorePolicyAsync(StorePolicy storePolicy);
    Task<StorePolicy?> GetPolicyByIdAsync(Guid policyId);
    Task<StorePolicy?> GetActivePolicyForBookstoreAsync(Guid bookstoreId);
}
