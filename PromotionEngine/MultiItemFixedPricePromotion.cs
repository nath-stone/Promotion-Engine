namespace PromotionEngine
{
    public class MultiItemFixedPricePromotion : IPromotion
    {
        public IReadOnlyCollection<string> ProductIds { get; }
        public decimal FixedPrice { get; }

        public MultiItemFixedPricePromotion(IReadOnlyCollection<string> productIds, decimal fixedPrice)
        {
            ProductIds = productIds;
            FixedPrice = fixedPrice;
        }

        public bool IsSatisfied(IEnumerable<Product> products)
        {
            return ProductIds.All(productId => products.Any(product => product.Id == productId));
        }

        public decimal CalculateReduction(IEnumerable<Product> products)
        {
            var applicableItems = products.Where(x => ProductIds.Contains(x.Id)).ToList();

            var fewestApplicableItemCount = 0;
            var fewestApplicableItemCountSet = false;

            foreach (var productId in ProductIds)
            {
                var itemCount = applicableItems.Count(x => x.Id == productId);

                if (!fewestApplicableItemCountSet)
                {
                    fewestApplicableItemCount = itemCount;
                    fewestApplicableItemCountSet = true;
                    continue;
                }

                if (itemCount < fewestApplicableItemCount)
                    fewestApplicableItemCount = itemCount;
            }

            if (fewestApplicableItemCount == 0)
                return 0;

            var itemsNotApplicable = new List<Product>();

            foreach (var item in applicableItems)
            {
                if (itemsNotApplicable.Count(x => x.Id == item.Id) == fewestApplicableItemCount)
                    itemsNotApplicable.Add(item);
            }

            var itemTotal = applicableItems.Sum(x => x.Price);
            var itemsNotApplicableTotal = itemsNotApplicable.Sum(x => x.Price);

            return itemTotal - itemsNotApplicableTotal - (fewestApplicableItemCount * FixedPrice);
        }
    }
}
