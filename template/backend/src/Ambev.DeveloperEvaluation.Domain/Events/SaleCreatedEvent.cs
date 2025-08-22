using Ambev.DeveloperEvaluation.Common.Domain;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Domain event raised when a sale is created.
/// </summary>
public class SaleCreatedEvent : IDomainEvent, INotification
{
    /// <summary>
    /// Gets the ID of the created sale.
    /// </summary>
    public Guid SaleId { get; }

    /// <summary>
    /// Gets the sale number of the created sale.
    /// </summary>
    public string SaleNumber { get; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }

    /// <summary>
    /// Initializes a new instance of the SaleCreatedEvent class.
    /// </summary>
    /// <param name="saleId">The ID of the created sale.</param>
    /// <param name="saleNumber">The sale number of the created sale.</param>
    public SaleCreatedEvent(Guid saleId, string saleNumber)
    {
        SaleId = saleId;
        SaleNumber = saleNumber;
        OccurredOn = DateTime.UtcNow;
    }
}
