using codex_backend.Application.Authorization.Common.Exceptions;
using codex_backend.Application.Dtos;
using codex_backend.Application.Factories;
using codex_backend.Application.Repositories.Interfaces;
using codex_backend.Enums;
using codex_backend.Helpers;
using codex_backend.Models;

namespace codex_backend.Application.Factories;

public class ReservationFactory(
    IBookItemRepository bookItemRepo,
    IStorePolicyPricesRepository policyPriceRepo,
    IStorePolicyRepository policyRepo
    ) : IReservationFactory
{
    private readonly IBookItemRepository _bookItemRepo = bookItemRepo;
    private readonly IStorePolicyPricesRepository _policyPriceRepo = policyPriceRepo;
    private readonly IStorePolicyRepository _policyRepo = policyRepo;

    public async Task<Reservation> CreateReservationAsync(ReservationCreateDto dto, Guid userId)
    {
        var bookItem = await _bookItemRepo.GetBookItemByIdAsync(dto.BookItemId)
            ?? throw new NotFoundException("Bookitem not found");

        var bookstoreId = bookItem.BookstoreId; 

        var activePolicy = await _policyRepo.GetActivePolicyForBookstoreAsync(bookstoreId)
            ?? throw new NotFoundException("No pricing policy found for this bookstore.");

        var policyPrice = await _policyPriceRepo.GetPriceByPolicyAndDurationAsync(activePolicy.Id, dto.DurationInMonths)
            ?? throw new NotFoundException($"There is no rental option for {dto.DurationInMonths} months.");

        var basePrice = policyPrice.Price;
        var dueDate = dto.PickupDate.AddMonths(policyPrice.DurationInMonths);
        var finalPrice = PriceCalculationHelper.CalculateFinalPrice(basePrice, bookItem.Condition);

        return new Reservation
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            BookItemId = dto.BookItemId,
            PoliciesId = activePolicy.Id,
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