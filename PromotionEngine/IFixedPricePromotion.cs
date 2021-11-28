namespace PromotionEngine
{
    public interface IFixedPricePromotion : IPromotion
    {
        decimal FixedPrice { get; }
    }
}
