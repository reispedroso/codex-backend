namespace codex_backend.Models;

public class StorePolicy
{
    public Guid Id { get; set; }
    public Guid BookstoreId { get; set; }

    public decimal LateFeePerDay { get; set; }
    public int GracePeriodDays { get; set; }
    public int MaxRenewals { get; set; }

    public ICollection<StorePolicyPrice> Prices { get; set; } = [];
}
