using codex_backend.Application.Dtos;
using codex_backend.Enums;

namespace codex_backend.Application.Validators;

public static class ReservationValidator
{
    public static IReadOnlyList<string> ValidateReservation(ReservationCreateDto dto)
    {
        var errors = new List<string>();

        if (dto.BookItemId == Guid.Empty)
            errors.Add("BookItemId is required");

        if (dto.DurationInMonths <= 0)
            errors.Add("DurationInMonths must be a positive number.");

        if (dto.PickupDate == default)
            errors.Add("PickupDate must be set");
        else if (dto.PickupDate < DateTime.UtcNow)
            errors.Add("PickupDate cannot be in the past");

        return errors;
    }
}
