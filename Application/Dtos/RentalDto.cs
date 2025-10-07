using codex_backend.Enums;

namespace codex_backend.Application.Dtos;

public class RentalCreateDto
{
    public Guid ReservationId { get; set; }
    // public Guid UserId { get; set; }
    // public RentalStatus Status { get; set; }
    // public DateTime RentedAt { get; set; }
    // public DateTime DueDate { get; set; }
    // public DateTime? ReturnedAt { get; set; }
    // public int LateDays { get; set; } = 0;
    // public decimal LateFeeAmount { get; set; } = 0.00m;
    // public string CurrencyCode { get; set; } = "BRL";
    // public decimal PriceAmount { get; set; }
    // public DateTime CreatedAt { get; set; }
}

public class RentalReadDto
{
    public Guid Id { get; set; }
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }
    public RentalStatus Status { get; set; }
    public DateTime RentedAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public int LateDays { get; set; }
    public decimal LateFeeAmount { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal PriceAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}

public class RentalUpdateDto
{
    public Guid ReservationId { get; set; }
    public Guid UserId { get; set; }
    public RentalStatus Status { get; set; }
    public DateTime RentedAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public int LateDays { get; set; } = 0;
    public decimal LateFeeAmount { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal PriceAmount { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}