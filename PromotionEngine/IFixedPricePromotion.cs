namespace PromotionEngine
{
    public interface IFixedPricePromotion : IPromotion
    {
        /// <summary>
        /// The fixed price for all of the items that qualify for the promotion.
        /// </summary>
        decimal FixedPrice { get; }
    }
}
