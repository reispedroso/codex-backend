namespace codex_backend.Application.Validators;

public class RentalValidator
{
    public static void ValidateDate(DateTime rentDate, DateTime returnDate)
    {
        if (rentDate > returnDate)
        {
            throw new ArgumentException("Rent date cannot be after return date");
        }
        
        if (rentDate < DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Utc))
            throw new ArgumentException("Rent date cannot be in the past");

        if (returnDate < DateTime.SpecifyKind(DateTime.UtcNow.Date, DateTimeKind.Utc))
            throw new ArgumentException("Return date cannot be in the past");
    }
}