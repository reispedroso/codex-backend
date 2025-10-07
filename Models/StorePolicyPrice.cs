using codex_backend.Enums;

namespace codex_backend.Models;

public class StorePolicyPrice
{
    public Guid Id { get; set; }
    public Guid StorePolicyId { get; set; }

    public int DurationInMonths { get; set; }
    public CurrencyCode Currency { get; set; }
    public decimal Price { get; set; }

    public StorePolicy? StorePolicy { get; set; }
}
