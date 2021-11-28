namespace PromotionEngine;

public interface ICart
{
    /// <summary>
    /// List of products in the cart.
    /// </summary>
    IList<IProduct> Items { get; }
    
    /// <summary>
    /// List of promotions that have been applied to the items in the cart.
    /// </summary>
    IList<IPromotion> AppliedPromotions { get; }

    /// <summary>
    /// Calculates the total cost for the items in the cart with the discounts from promotions applied.
    /// </summary>
    /// <returns>The total cost for the items with promotion discounts applied.</returns>
    decimal CalculateTotal();
}