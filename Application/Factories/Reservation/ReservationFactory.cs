using codex_backend.Application.Common.Exceptions;
using codex_backend.Application.Dtos;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Enums;
using codex_backend.Helpers;
using codex_backend.Models;

namespace codex_backend.Application.Factories;

public class ReservationFactory(
    IBookItemRepository bookItemRepo,
    IStorePolicyPricesRepository policyPriceRepo
    ) : IReservationFactory
{
    private readonly IBookItemRepository bookItemRepo = bookItemRepo;
    private readonly IStorePolicyPricesRepository _policyPriceRepo = policyPriceRepo;
    public async Task<Reservation> CreateReservationAsync(ReservationCreateDto dto, Guid userId)
    {
        var policyPrice = await _policyPriceRepo.GetPolicyPriceByIdAsync(dto.PriceId)
        ?? throw new NotFoundException("Policy not found");

        var bookItem = await bookItemRepo.GetBookItemByIdAsync(dto.BookItemId)
        ?? throw new NotFoundException("Bookitem not found");

        var basePrice = policyPrice.Price;

        var dueDate = dto.PickupDate.AddMonths(policyPrice.DurationInMonths);

        var finalPrice = PriceCalculationHelper.CalculateFinalPrice(basePrice, bookItem.Condition);

        return new Reservation
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            BookItemId = dto.BookItemId,
            PoliciesId = policyPrice.StorePolicyId,
            Status = ReservationStatus.Pending,
            PickupDate = dto.PickupDate,
            DueDate = dueDate,
            PriceAmountSnapshot = finalPrice,
            CurrencySnapshot = policyPrice.Currency.ToString(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}