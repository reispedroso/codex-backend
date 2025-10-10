namespace codex_backend.Application.Dtos;


public class StorePolicyCreateDto
{
    public Guid BookstoreId { get; set; }
    public decimal LateFeePerDay { get; set; }
    public int GracePeriodDays { get; set; }
    public int MaxRenewals { get; set; }
    public ICollection<StorePolicyPriceDto> Prices { get; set; } = [];
}

public class StorePolicyUpdateDto
{
    public decimal LateFeePerDay { get; set; }
    public int GracePeriodDays { get; set; }
    public int MaxRenewals { get; set; }
}


public class StorePolicyReadDto
{
    public Guid Id { get; set; }
    public Guid BookstoreId { get; set; }
    public decimal LateFeePerDay { get; set; }
    public int GracePeriodDays { get; set; }
    public int MaxRenewals { get; set; }
    
    public ICollection<StorePolicyPriceDto> Prices { get; set; } = [];
}