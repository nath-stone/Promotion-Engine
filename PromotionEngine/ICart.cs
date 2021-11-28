namespace PromotionEngine;

public interface ICart
{
    IList<IProduct> Items { get; }
    IList<IPromotion> AppliedPromotions { get; }
    decimal CalculateTotal();
}