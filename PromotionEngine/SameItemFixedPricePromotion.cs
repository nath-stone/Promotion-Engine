namespace PromotionEngine
{
    public class SameItemFixedPricePromotion : IFixedPricePromotion
    {
        public string ProductId { get; }
        public int CountRequired { get; }
        public decimal FixedPrice { get; }

        public SameItemFixedPricePromotion(string productId, int countRequired, decimal fixedPrice)
        {
            ProductId = productId;
            CountRequired = countRequired;
            FixedPrice = fixedPrice;
        }

        public bool IsSatisfied(IEnumerable<IProduct> products)
        {
            var numberOfApplicableItems = products.Count(x => x.Id == ProductId);
            return numberOfApplicableItems >= CountRequired;
        }

        public decimal CalculateReduction(IEnumerable<IProduct> products)
        {
            var applicableItems = products.Where(x => x.Id == ProductId).ToList();

            var numberOfApplicableItems = applicableItems.Count(x => x.Id == ProductId);

            if (numberOfApplicableItems < CountRequired)
                return 0;

            var promotionApplyCount = (int)Math.Floor((double)numberOfApplicableItems / CountRequired);
            var itemTotal = applicableItems.Sum(x => x.Price);
            var itemsNotApplicable = applicableItems.Take(numberOfApplicableItems % CountRequired);
            var itemsNotApplicableTotal = itemsNotApplicable.Sum(x => x.Price);

            return itemTotal - itemsNotApplicableTotal - (promotionApplyCount * FixedPrice);
        }
    }
}
