namespace codex_backend.Enums;
 public enum ReservationStatus
    {
        Pending,   // usuário fez a reserva, mas ainda não chegou a hora
        Ready,     // já pode retirar (janela de pickup chegou)
        Rented, // reserva virou rental
        Cancelled, // cancelada antes de virar aluguel
        Expired    // usuário não compareceu no prazo
    }