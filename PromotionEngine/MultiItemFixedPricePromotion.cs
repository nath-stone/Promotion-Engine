namespace PromotionEngine
{
    public class MultiItemFixedPricePromotion : IFixedPricePromotion
    {
        public IReadOnlyCollection<string> ProductIds { get; }
        public decimal FixedPrice { get; }

        public MultiItemFixedPricePromotion(IReadOnlyCollection<string> productIds, decimal fixedPrice)
        {
            ProductIds = productIds;
            FixedPrice = fixedPrice;
        }

        public bool IsSatisfied(IEnumerable<IProduct> products)
        {
            return ProductIds.All(productId => products.Any(product => product.Id == productId));
        }

        public decimal CalculateReduction(IEnumerable<IProduct> products)
        {
            var relevantItems = products.Where(x => ProductIds.Contains(x.Id)).ToList();

            var fewestApplicableItemCount = GetFewestApplicableItemCount(relevantItems);

            if (fewestApplicableItemCount == 0)
                return 0;

            var itemsNotApplicable = GetItemsNotApplicable(relevantItems, fewestApplicableItemCount);
            var itemTotal = relevantItems.Sum(x => x.Price);
            var itemsNotApplicableTotal = itemsNotApplicable.Sum(x => x.Price);

            return itemTotal - itemsNotApplicableTotal - (fewestApplicableItemCount * FixedPrice);
        }

        private int GetFewestApplicableItemCount(IList<IProduct> items)
        {
            var fewestApplicableItemCount = 0;
            var fewestApplicableItemCountSet = false;

            foreach (var productId in ProductIds)
            {
                var itemCount = items.Count(x => x.Id == productId);

                if (!fewestApplicableItemCountSet)
                {
                    fewestApplicableItemCount = itemCount;
                    fewestApplicableItemCountSet = true;
                    continue;
                }

                if (itemCount < fewestApplicableItemCount)
                    fewestApplicableItemCount = itemCount;
            }

            return fewestApplicableItemCount;
        }

        private IEnumerable<IProduct> GetItemsNotApplicable(IEnumerable<IProduct> items, int fewestApplicableCount)
        {
            var itemsNotApplicable = new List<IProduct>();
            
            foreach (var itemGroup in items.GroupBy(x => x.Id))
                itemsNotApplicable.AddRange(itemGroup.Skip(fewestApplicableCount));

            return itemsNotApplicable;
        }
    }
}
