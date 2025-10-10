using codex_backend.Application.Dtos;

namespace codex_backend.Application.Services.Interfaces;

public interface IStorePolicyService
{
    Task<StorePolicyReadDto?> CreatePolicyAsync(StorePolicyCreateDto createDto);
    Task<bool> UpdatePolicyAsync(Guid policyId, StorePolicyUpdateDto updateDto);
    Task<StorePolicyReadDto?> GetPolicyByIdAsync(Guid policyId);
    Task<StorePolicyReadDto?> GetActivePolicyForBookstoreAsync(Guid bookstoreId);
    Task<LateFeeCalculationResultDto?> CalculateLateFeeAsync(LateFeeCalculationRequestDto requestDto);
}