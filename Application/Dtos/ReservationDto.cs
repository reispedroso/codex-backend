using codex_backend.Enums;

namespace codex_backend.Application.Dtos
{
    // DTO usado quando o cliente cria uma reserva
    public class ReservationCreateDto
    {
        public Guid UserId { get; set; }
        public Guid BookItemId { get; set; }
        public Guid PoliciesId { get; set; }
        public Guid PriceId { get; set; } 
        public DateTime PickupDate { get; set; }

    }

    // DTO usado para leitura (resposta para o cliente)
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

    // DTO usado quando o cliente/sistema atualiza uma reserva
    public class ReservationUpdateDto
    {
        // Se o cliente pode mudar status (ex.: cancelar)
        public ReservationStatus Status { get; set; }

        // Se for permitido reagendar
        public DateTime PickupDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
