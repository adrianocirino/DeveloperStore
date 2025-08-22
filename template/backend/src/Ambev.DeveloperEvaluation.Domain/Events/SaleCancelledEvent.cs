using Ambev.DeveloperEvaluation.Common.Domain;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Domain event raised when a sale is cancelled.
/// </summary>
public class SaleCancelledEvent : IDomainEvent, INotification
{
    /// <summary>
    /// Gets the ID of the cancelled sale.
    /// </summary>
    public Guid SaleId { get; }

    /// <summary>
    /// Gets the sale number of the cancelled sale.
    /// </summary>
    public string SaleNumber { get; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }

    /// <summary>
    /// Initializes a new instance of the SaleCancelledEvent class.
    /// </summary>
    /// <param name="saleId">The ID of the cancelled sale.</param>
    /// <param name="saleNumber">The sale number of the cancelled sale.</param>
    public SaleCancelledEvent(Guid saleId, string saleNumber)
    {
        SaleId = saleId;
        SaleNumber = saleNumber;
        OccurredOn = DateTime.UtcNow;
    }
}
