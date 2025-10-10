using codex_backend.Enums;
using System;

namespace codex_backend.Helpers
{
    public static class PriceCalculationHelper
    {
        public static decimal CalculateFinalPrice(decimal basePrice, BookCondition condition)
        {
            decimal modifier = condition switch
            {

                BookCondition.New => 1.00m,
                BookCondition.LikeNew => 1.00m,

                // Descontos progressivos para condições inferiores
                BookCondition.VeryGood => 0.95m, // 5% de desconto
                BookCondition.Good => 0.90m,     // 10% de desconto
                BookCondition.Acceptable => 0.80m, // 20% de desconto
                BookCondition.Poor => 0.60m,       // 40% de desconto
                
                // Um livro danificado poderia até ser impedido de alugar,
                // mas por enquanto, aplicamos o maior desconto.
                BookCondition.Damaged => 0.50m,    // 50% de desconto
                _ => 1.00m
            };

            var finalPrice = basePrice * modifier;

            // Arredondamos para 2 casas decimais, o padrão para valores monetários.
            return Math.Round(finalPrice, 2);
        }
    }
}