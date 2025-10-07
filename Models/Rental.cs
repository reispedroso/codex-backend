using codex_backend.Enums;

namespace codex_backend.Models
{
    public class Rental
    {
        public Guid Id { get; set; }

        public Guid ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public RentalStatus Status { get; set; }

        // Datas efetivas da locação
        public DateTime RentedAt { get; set; }     // quando realmente pegou o livro
        public DateTime DueDate { get; set; }      // prazo oficial de devolução
        public DateTime? ReturnedAt { get; set; }  // quando devolveu (se já devolveu)

        // Custos da reserva
        public string? CurrencyCode { get; set; }
        public decimal PriceAmount { get; set; }


        // Custos adicionais
        public int LateDays { get; set; }          // dias em atraso (se houver)
        public decimal LateFeeAmount { get; set; } // multa por atraso

        // Controle de ciclo de vida

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
