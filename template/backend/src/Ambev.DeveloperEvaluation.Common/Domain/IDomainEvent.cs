namespace Ambev.DeveloperEvaluation.Common.Domain;

/// <summary>
/// Marker interface for domain events.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    DateTime OccurredOn { get; }
}
