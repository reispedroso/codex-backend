using codex_backend.Application.Dtos;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Application.Services.Interfaces;
using codex_backend.Models;

namespace codex_backend.Application.Services.Implementations;

public class StorePolicyService(IStorePolicyRepository storePolicyRepository) : IStorePolicyService
{
    private readonly IStorePolicyRepository _storePolicyRepository = storePolicyRepository;

    public async Task<StorePolicyReadDto?> CreatePolicyAsync(StorePolicyCreateDto createDto)
    {
        var existingPolicy = await _storePolicyRepository.GetActivePolicyForBookstoreAsync(createDto.BookstoreId);
        if (existingPolicy is not null)
        {
            return null;
        }

        var policy = new StorePolicy
        {
            BookstoreId = createDto.BookstoreId,
            LateFeePerDay = createDto.LateFeePerDay,
            GracePeriodDays = createDto.GracePeriodDays,
            MaxRenewals = createDto.MaxRenewals,
            Prices = [.. createDto.Prices.Select(p => new StorePolicyPrice
            {
                DurationInMonths = p.DurationInMonths,
                Currency = p.Currency,
                Price = p.Price
            })]
        };

        var createdPolicy = await _storePolicyRepository.CreateStorePolicyAsync(policy);
        return MapToReadDto(createdPolicy);

    }

    public async Task<bool> UpdatePolicyAsync(Guid policyId, StorePolicyUpdateDto updateDto)
    {
        var policyToUpdate = await _storePolicyRepository.GetPolicyByIdAsync(policyId);

        if (policyToUpdate == null)
        {
            return false;
        }

        policyToUpdate.LateFeePerDay = updateDto.LateFeePerDay;
        policyToUpdate.GracePeriodDays = updateDto.GracePeriodDays;
        policyToUpdate.MaxRenewals = updateDto.MaxRenewals;

        return await _storePolicyRepository.UpdateStorePolicyAsync(policyToUpdate);
    }

    public async Task<StorePolicyReadDto?> GetPolicyByIdAsync(Guid policyId)
    {
        var policy = await _storePolicyRepository.GetPolicyByIdAsync(policyId);
        return policy == null ? null : MapToReadDto(policy);
    }

    public async Task<StorePolicyReadDto?> GetActivePolicyForBookstoreAsync(Guid bookstoreId)
    {
        var policy = await _storePolicyRepository.GetActivePolicyForBookstoreAsync(bookstoreId);
        return policy == null ? null : MapToReadDto(policy);
    }


    public async Task<LateFeeCalculationResultDto?> CalculateLateFeeAsync(LateFeeCalculationRequestDto requestDto)
    {
        var policy = await _storePolicyRepository.GetActivePolicyForBookstoreAsync(requestDto.BookstoreId);
        if (policy is null)
        {
            return null;
        }

        int billableDays = Math.Max(0, requestDto.DaysOverdue - policy.GracePeriodDays);
        decimal totalFee = billableDays * policy.LateFeePerDay;

        var result = new LateFeeCalculationResultDto
        {
            FeePerDay = policy.LateFeePerDay,
            GracePeriodDays = policy.GracePeriodDays,
            OverdueDays = requestDto.DaysOverdue,
            BillableDays = billableDays,
            TotalFee = totalFee,
            Message = billableDays > 0
            ? $"Late fee calculated for {billableDays} day(s) after the grace period of {policy.GracePeriodDays} day(s)."
            : "No late fee to be charged. The delay is within the grace period."
        };

        return result;
    }

    private static StorePolicyReadDto MapToReadDto(StorePolicy policy)
    {
        return new StorePolicyReadDto
        {
            Id = policy.Id,
            BookstoreId = policy.BookstoreId,
            LateFeePerDay = policy.LateFeePerDay,
            GracePeriodDays = policy.GracePeriodDays,
            MaxRenewals = policy.MaxRenewals,
            Prices = [.. policy.Prices.Select(p => new StorePolicyPriceDto
            {
                DurationInMonths = p.DurationInMonths,
                Currency = p.Currency,
                Price = p.Price
            })]
        };
    }
}

