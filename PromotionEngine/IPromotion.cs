namespace PromotionEngine
{
    public interface IPromotion
    {
        /// <summary>
        /// Checks whether or not a promotion is applicable to a group of products.
        /// </summary>
        /// <param name="products">Products to check for the promotion.</param>
        /// <returns>True if the promotion is applicable, otherwise false.</returns>
        public bool IsSatisfied(IEnumerable<Product> products);

        /// <summary>
        /// Calculates how much money is saved from this promotion.
        /// </summary>
        /// <param name="products">The products to calculate the reduction for.</param>
        /// <returns>How much money is saved from this promotion.</returns>
        public decimal CalculateReduction(IEnumerable<Product> products);
    }
}
