namespace codex_backend.Application.Dtos;

public class StorePolicyCreateDto
{
    public Guid BookstoreId { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal LateFeePerDay { get; set; }
    public int GracePeriodDays { get; set; }
    public int MaxRenawals { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class StorePolicyReadDto
{
    public Guid Id { get; set; }
    public Guid BookstoreId { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal LateFeePerDay { get; set; }
    public int GracePeriodDays { get; set; }
    public int MaxRenawals { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}

public class StorePolicyUpdateDto
{
    public Guid BookstoreId { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal LateFeePerDay { get; set; }
    public int GracePeriodDays { get; set; }
    public int MaxRenawals { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}