namespace PromotionEngine
{
    public class Cart
    {
        public IList<Product> Items { get; } = new List<Product>();

        public IList<IPromotion> AppliedPromotions { get; } = new List<IPromotion>();

        public decimal CalculateTotal()
        {
            var itemsTotal = Items.Sum(x => x.Price);
            var reduction = AppliedPromotions.Sum(x => x.CalculateReduction(Items));
            return itemsTotal - reduction;
        }
    }
}
