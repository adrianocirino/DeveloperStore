using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an individual item in a sale.
/// This entity handles quantity-based discount calculations according to business rules.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// Gets the product information.
    /// </summary>
    public Product Product { get; private set; } = null!;

    /// <summary>
    /// Gets the quantity of the product.
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// Gets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// Gets the discount percentage applied to this item.
    /// </summary>
    public decimal DiscountPercentage { get; private set; }

    /// <summary>
    /// Gets the total amount for this item (including discounts).
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// Gets the sale ID this item belongs to.
    /// </summary>
    public Guid SaleId { get; private set; }

    /// <summary>
    /// Initializes a new instance of the SaleItem class.
    /// </summary>
    public SaleItem()
    {
    }

    /// <summary>
    /// Creates a new sale item with the specified details.
    /// </summary>
    /// <param name="product">The product information.</param>
    /// <param name="quantity">The quantity of the product.</param>
    /// <param name="unitPrice">The unit price of the product.</param>
    /// <param name="saleId">The ID of the sale this item belongs to.</param>
    /// <returns>A new SaleItem instance.</returns>
    public static SaleItem Create(Product product, int quantity, decimal unitPrice, Guid saleId)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }

        if (quantity > 20)
        {
            throw new InvalidOperationException("Cannot sell more than 20 identical items.");
        }

        if (unitPrice <= 0)
        {
            throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPrice));
        }

        var item = new SaleItem
        {
            Product = product,
            Quantity = quantity,
            UnitPrice = unitPrice,
            SaleId = saleId
        };

        item.CalculateDiscount();
        item.CalculateTotalAmount();

        return item;
    }

    /// <summary>
    /// Updates the quantity of the item.
    /// </summary>
    /// <param name="newQuantity">The new quantity.</param>
    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(newQuantity));
        }

        if (newQuantity > 20)
        {
            throw new InvalidOperationException("Cannot sell more than 20 identical items.");
        }

        Quantity = newQuantity;
        CalculateDiscount();
        CalculateTotalAmount();
    }

    /// <summary>
    /// Updates the unit price of the item.
    /// </summary>
    /// <param name="newUnitPrice">The new unit price.</param>
    public void UpdateUnitPrice(decimal newUnitPrice)
    {
        if (newUnitPrice <= 0)
        {
            throw new ArgumentException("Unit price must be greater than zero.", nameof(newUnitPrice));
        }

        UnitPrice = newUnitPrice;
        CalculateTotalAmount();
    }

    /// <summary>
    /// Calculates the discount percentage based on quantity according to business rules.
    /// </summary>
    private void CalculateDiscount()
    {
        // Business rules:
        // - Purchases below 4 items cannot have a discount
        // - Purchases above 4 identical items have a 10% discount
        // - Purchases between 10 and 20 identical items have a 20% discount

        if (Quantity < 4)
        {
            DiscountPercentage = 0;
        }
        else if (Quantity >= 10 && Quantity <= 20)
        {
            DiscountPercentage = 20;
        }
        else if (Quantity >= 4)
        {
            DiscountPercentage = 10;
        }
    }

    /// <summary>
    /// Calculates the total amount for this item including discounts.
    /// </summary>
    private void CalculateTotalAmount()
    {
        var subtotal = Quantity * UnitPrice;
        var discountAmount = subtotal * (DiscountPercentage / 100);
        TotalAmount = subtotal - discountAmount;
    }
}
