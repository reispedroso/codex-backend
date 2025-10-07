using codex_backend.Enums;

namespace codex_backend.Helpers;

public static class PriceCalculationHelper
{
    public static decimal CalculateFinalPrice(decimal basePrice, BookCondition condition)
    {
        var multiplier = condition switch
        {
            BookCondition.New => 1.0m,
            BookCondition.LikeNew => 0.9m,
            BookCondition.VeryGood => 0.85m,
            BookCondition.Good => 0.8m,
            BookCondition.Acceptable => 0.7m,
            BookCondition.Poor => 0.5m,
            BookCondition.Damaged => 0.4m,
            _ => 1.0m
        };

        return basePrice * multiplier;
    }
}