namespace codex_backend.Application.Dtos;
public class LateFeeCalculationRequestDto
{ 
    public Guid BookstoreId { get; set; }
    public int DaysOverdue { get; set; }
}

public class LateFeeCalculationResultDto
{
    public decimal FeePerDay { get; set; }
    public int GracePeriodDays { get; set; }
    public int OverdueDays { get; set; }
    public int BillableDays { get; set; }
    public decimal TotalFee { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}