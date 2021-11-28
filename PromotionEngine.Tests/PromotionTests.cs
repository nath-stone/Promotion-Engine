using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PromotionEngine.Tests
{
    [TestClass]
    public class PromotionTests
    {
        private readonly IReadOnlyDictionary<string, Product> _products = new Dictionary<string, Product>
                                                                          {
                                                                              { "A", new Product("A", 50) },
                                                                              { "B", new Product("B", 30) },
                                                                              { "C", new Product("C", 20) },
                                                                              { "D", new Product("D", 15) }
                                                                          };

        private readonly IEnumerable<IPromotion> _promotions = new List<IPromotion>
                                                               {
                                                               };

        [TestMethod]
        [DataRow("100", "A", "B", "C")]
        public void FixedPricePromotions(string expectedTotalString, params string[] cartItemIds)
        {
            var expectedTotal = decimal.Parse(expectedTotalString);
            var cart = new Cart();

            foreach (var cartItemId in cartItemIds)
                cart.Items.Add(_products[cartItemId]);

            foreach (var promotion in _promotions)
            {
                if (promotion.IsSatisfied(cart.Items))
                    cart.AppliedPromotions.Add(promotion);
            }

            var calculatedCartTotal = cart.CalculateTotal();

            Assert.AreEqual(expectedTotal, calculatedCartTotal);
        }
    }
}
