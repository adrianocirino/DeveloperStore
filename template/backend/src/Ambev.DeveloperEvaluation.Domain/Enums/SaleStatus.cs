namespace Ambev.DeveloperEvaluation.Domain.Enums;

/// <summary>
/// Represents the status of a sale.
/// </summary>
public enum SaleStatus
{
    /// <summary>
    /// The sale is active and can be modified.
    /// </summary>
    Active = 1,

    /// <summary>
    /// The sale has been cancelled and cannot be modified.
    /// </summary>
    Cancelled = 2
}
