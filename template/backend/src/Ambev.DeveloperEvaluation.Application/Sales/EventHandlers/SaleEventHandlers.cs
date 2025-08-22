using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.EventHandlers;

/// <summary>
/// Handlers for sale domain events.
/// </summary>
public class SaleEventHandlers :
    INotificationHandler<SaleCreatedEvent>,
    INotificationHandler<SaleModifiedEvent>,
    INotificationHandler<SaleCancelledEvent>,
    INotificationHandler<ItemCancelledEvent>
{
    private readonly ILogger<SaleEventHandlers> _logger;

    /// <summary>
    /// Initializes a new instance of the SaleEventHandlers class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    public SaleEventHandlers(ILogger<SaleEventHandlers> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Handles the SaleCreatedEvent.
    /// </summary>
    /// <param name="notification">The sale created event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SaleCreated event received: Sale ID: {SaleId}, Sale Number: {SaleNumber}, Occurred at: {OccurredOn}",
            notification.SaleId, notification.SaleNumber, notification.OccurredOn);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles the SaleModifiedEvent.
    /// </summary>
    /// <param name="notification">The sale modified event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SaleModified event received: Sale ID: {SaleId}, Sale Number: {SaleNumber}, Occurred at: {OccurredOn}",
            notification.SaleId, notification.SaleNumber, notification.OccurredOn);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles the SaleCancelledEvent.
    /// </summary>
    /// <param name="notification">The sale cancelled event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("SaleCancelled event received: Sale ID: {SaleId}, Sale Number: {SaleNumber}, Occurred at: {OccurredOn}",
            notification.SaleId, notification.SaleNumber, notification.OccurredOn);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles the ItemCancelledEvent.
    /// </summary>
    /// <param name="notification">The item cancelled event.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ItemCancelled event received: Sale ID: {SaleId}, Item ID: {ItemId}, Occurred at: {OccurredOn}",
            notification.SaleId, notification.ItemId, notification.OccurredOn);

        return Task.CompletedTask;
    }
}
