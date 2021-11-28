namespace PromotionEngine
{
    public class Cart : ICart
    {
        public IList<IProduct> Items { get; } = new List<IProduct>();

        public IList<IPromotion> AppliedPromotions { get; } = new List<IPromotion>();

        public decimal CalculateTotal()
        {
            var itemsTotal = Items.Sum(x => x.Price);
            var reduction = AppliedPromotions.Sum(x => x.CalculateReduction(Items));
            return itemsTotal - reduction;
        }
    }
}
