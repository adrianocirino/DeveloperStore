using Ambev.DeveloperEvaluation.Common.Domain;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Domain event raised when an item is cancelled from a sale.
/// </summary>
public class ItemCancelledEvent : IDomainEvent, INotification
{
    /// <summary>
    /// Gets the ID of the sale containing the cancelled item.
    /// </summary>
    public Guid SaleId { get; }

    /// <summary>
    /// Gets the ID of the cancelled item.
    /// </summary>
    public Guid ItemId { get; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }

    /// <summary>
    /// Initializes a new instance of the ItemCancelledEvent class.
    /// </summary>
    /// <param name="saleId">The ID of the sale containing the cancelled item.</param>
    /// <param name="itemId">The ID of the cancelled item.</param>
    public ItemCancelledEvent(Guid saleId, Guid itemId)
    {
        SaleId = saleId;
        ItemId = itemId;
        OccurredOn = DateTime.UtcNow;
    }
}
