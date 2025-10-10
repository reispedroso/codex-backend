using codex_backend.Enums;

namespace codex_backend.Application.Dtos
{
    public class ReservationCreateDto
    {

        public Guid BookItemId { get; set; }
        public int DurationInMonths { get; set; }
        public DateTime PickupDate { get; set; }
    }

    public class ReservationUpdateDto
    {
        public Guid Id { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime PickupDate { get; set; }
    }


    public class ReservationReadDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookItemId { get; set; }
        public Guid PoliciesId { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal PriceAmountSnapshot { get; set; }
        public string CurrencySnapshot { get; set; } = "BRL";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}