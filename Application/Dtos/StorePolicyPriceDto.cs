using codex_backend.Enums;
using System.ComponentModel.DataAnnotations;

namespace codex_backend.Application.Dtos;

public class StorePolicyPriceDto
{
    [Range(1, 120)]
    public int DurationInMonths { get; set; }
    public CurrencyCode Currency { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Price { get; set; }
}