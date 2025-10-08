using codex_backend.Application.Common.Exceptions;

namespace codex_backend.Application.Validators;

public class RentalValidator
{
    public static void ValidateDate(DateTime rentDate, DateTime returnDate)
    {
        if (rentDate > returnDate)
        {
            throw new ArgException("Rent date cannot be after return date");
        }
        
        if (rentDate < DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Utc))
            throw new ArgException("Rent date cannot be in the past");

        if (returnDate < DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Utc))
            throw new ArgException("Return date cannot be in the past");
    }
}