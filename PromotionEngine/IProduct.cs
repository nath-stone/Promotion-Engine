namespace PromotionEngine;

public interface IProduct
{
    /// <summary>
    /// The ID of the product.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// The price of the product.
    /// </summary>
    decimal Price { get; }
}