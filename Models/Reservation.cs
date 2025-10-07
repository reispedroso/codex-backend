using codex_backend.Enums;

namespace codex_backend.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }
        
        public Guid UserId { get; set; }
        public Guid BookItemId { get; set; }
        public Guid PoliciesId { get; set; }

        public ReservationStatus Status { get; set; }

        // Datas da reserva
        public DateTime PickupDate { get; set; }   // data que o usuário pode retirar
        public DateTime DueDate { get; set; }      // prazo planejado de devolução (snapshot)

        // Snapshot de preço no momento da reserva
        public decimal PriceAmountSnapshot { get; set; }
        public string? CurrencySnapshot { get; set; }

        // Relacionamentos
        public User? User { get; set; }
        public BookItem? BookItem { get; set; }
        public StorePolicy? StorePolicy { get; set; }

        // Controle de ciclo de vida
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
