using Ambev.DeveloperEvaluation.Common.Domain;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// <summary>
/// Domain event raised when a sale is modified.
/// </summary>
public class SaleModifiedEvent : IDomainEvent, INotification
{
    /// <summary>
    /// Gets the ID of the modified sale.
    /// </summary>
    public Guid SaleId { get; }

    /// <summary>
    /// Gets the sale number of the modified sale.
    /// </summary>
    public string SaleNumber { get; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }

    /// <summary>
    /// Initializes a new instance of the SaleModifiedEvent class.
    /// </summary>
    /// <param name="saleId">The ID of the modified sale.</param>
    /// <param name="saleNumber">The sale number of the modified sale.</param>
    public SaleModifiedEvent(Guid saleId, string saleNumber)
    {
        SaleId = saleId;
        SaleNumber = saleNumber;
        OccurredOn = DateTime.UtcNow;
    }
}
