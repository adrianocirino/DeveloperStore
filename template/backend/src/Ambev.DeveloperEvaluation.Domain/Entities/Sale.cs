using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale record in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets the sale number (unique identifier for the sale).
    /// </summary>
    public string SaleNumber { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the date when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; private set; }

    /// <summary>
    /// Gets the customer information.
    /// </summary>
    public Customer Customer { get; private set; } = null!;

    /// <summary>
    /// Gets the total sale amount.
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// Gets the branch where the sale was made.
    /// </summary>
    public Branch Branch { get; private set; } = null!;

    /// <summary>
    /// Gets the collection of sale items.
    /// </summary>
    public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

    /// <summary>
    /// Gets the sale status.
    /// </summary>
    public SaleStatus Status { get; private set; }

    /// <summary>
    /// Gets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Gets the date and time of the last update to the sale.
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    private readonly List<SaleItem> _items = new();

    /// <summary>
    /// Initializes a new instance of the Sale class.
    /// </summary>
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
        Status = SaleStatus.Active;
    }

    /// <summary>
    /// Creates a new sale with the specified details.
    /// </summary>
    /// <param name="saleNumber">The unique sale number.</param>
    /// <param name="customer">The customer information.</param>
    /// <param name="branch">The branch where the sale was made.</param>
    /// <param name="items">The collection of sale items.</param>
    /// <returns>A new Sale instance.</returns>
    public static Sale Create(string saleNumber, Customer customer, Branch branch, IEnumerable<SaleItem> items)
    {
        var sale = new Sale
        {
            SaleNumber = saleNumber,
            SaleDate = DateTime.UtcNow,
            Customer = customer,
            Branch = branch
        };

        foreach (var item in items)
        {
            sale.AddItem(item);
        }

        sale.CalculateTotalAmount();
        sale.AddDomainEvent(new SaleCreatedEvent(sale.Id, sale.SaleNumber));

        return sale;
    }

    /// <summary>
    /// Adds an item to the sale.
    /// </summary>
    /// <param name="item">The sale item to add.</param>
    public void AddItem(SaleItem item)
    {
        if (Status != SaleStatus.Active)
        {
            throw new InvalidOperationException("Cannot add items to a cancelled sale.");
        }

        _items.Add(item);
        CalculateTotalAmount();
    }

    /// <summary>
    /// Removes an item from the sale.
    /// </summary>
    /// <param name="itemId">The ID of the item to remove.</param>
    public void RemoveItem(Guid itemId)
    {
        if (Status != SaleStatus.Active)
        {
            throw new InvalidOperationException("Cannot remove items from a cancelled sale.");
        }

        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            _items.Remove(item);
            CalculateTotalAmount();
            AddDomainEvent(new ItemCancelledEvent(Id, itemId));
        }
    }

    /// <summary>
    /// Updates the sale with new information.
    /// </summary>
    /// <param name="customer">The new customer information.</param>
    /// <param name="branch">The new branch information.</param>
    public void Update(Customer customer, Branch branch)
    {
        if (Status != SaleStatus.Active)
        {
            throw new InvalidOperationException("Cannot update a cancelled sale.");
        }

        Customer = customer;
        Branch = branch;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new SaleModifiedEvent(Id, SaleNumber));
    }

    /// <summary>
    /// Cancels the sale.
    /// </summary>
    public void Cancel()
    {
        if (Status == SaleStatus.Cancelled)
        {
            throw new InvalidOperationException("Sale is already cancelled.");
        }

        Status = SaleStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new SaleCancelledEvent(Id, SaleNumber));
    }

    /// <summary>
    /// Calculates the total amount of the sale including discounts.
    /// </summary>
    private void CalculateTotalAmount()
    {
        TotalAmount = _items.Sum(item => item.TotalAmount);
    }
}
