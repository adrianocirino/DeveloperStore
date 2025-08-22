using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Sale entities.
/// </summary>
public interface ISaleRepository
{
    /// <summary>
    /// Gets a sale by its ID.
    /// </summary>
    /// <param name="id">The sale ID.</param>
    /// <returns>The sale if found; otherwise, null.</returns>
    Task<Sale?> GetByIdAsync(Guid id);

    /// <summary>
    /// Gets a sale by its sale number.
    /// </summary>
    /// <param name="saleNumber">The sale number.</param>
    /// <returns>The sale if found; otherwise, null.</returns>
    Task<Sale?> GetBySaleNumberAsync(string saleNumber);

    /// <summary>
    /// Gets all sales with optional filtering and pagination.
    /// </summary>
    /// <param name="page">The page number (default: 1).</param>
    /// <param name="size">The page size (default: 10).</param>
    /// <param name="orderBy">The ordering criteria.</param>
    /// <param name="customerId">Optional customer ID filter.</param>
    /// <param name="branchId">Optional branch ID filter.</param>
    /// <param name="status">Optional status filter.</param>
    /// <param name="minDate">Optional minimum date filter.</param>
    /// <param name="maxDate">Optional maximum date filter.</param>
    /// <param name="minAmount">Optional minimum amount filter.</param>
    /// <param name="maxAmount">Optional maximum amount filter.</param>
    /// <returns>A collection of sales.</returns>
    Task<IEnumerable<Sale>> GetAllAsync(
        int page = 1,
        int size = 10,
        string? orderBy = null,
        string? customerId = null,
        string? branchId = null,
        string? status = null,
        DateTime? minDate = null,
        DateTime? maxDate = null,
        decimal? minAmount = null,
        decimal? maxAmount = null);

    /// <summary>
    /// Adds a new sale.
    /// </summary>
    /// <param name="sale">The sale to add.</param>
    /// <returns>The added sale.</returns>
    Task<Sale> AddAsync(Sale sale);

    /// <summary>
    /// Updates an existing sale.
    /// </summary>
    /// <param name="sale">The sale to update.</param>
    /// <returns>The updated sale.</returns>
    Task<Sale> UpdateAsync(Sale sale);

    /// <summary>
    /// Deletes a sale.
    /// </summary>
    /// <param name="id">The ID of the sale to delete.</param>
    /// <returns>True if the sale was deleted; otherwise, false.</returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Checks if a sale number already exists.
    /// </summary>
    /// <param name="saleNumber">The sale number to check.</param>
    /// <returns>True if the sale number exists; otherwise, false.</returns>
    Task<bool> SaleNumberExistsAsync(string saleNumber);

    /// <summary>
    /// Gets the total count of sales matching the specified criteria.
    /// </summary>
    /// <param name="customerId">Optional customer ID filter.</param>
    /// <param name="branchId">Optional branch ID filter.</param>
    /// <param name="status">Optional status filter.</param>
    /// <param name="minDate">Optional minimum date filter.</param>
    /// <param name="maxDate">Optional maximum date filter.</param>
    /// <param name="minAmount">Optional minimum amount filter.</param>
    /// <param name="maxAmount">Optional maximum amount filter.</param>
    /// <returns>The total count of matching sales.</returns>
    Task<int> GetCountAsync(
        string? customerId = null,
        string? branchId = null,
        string? status = null,
        DateTime? minDate = null,
        DateTime? maxDate = null,
        decimal? minAmount = null,
        decimal? maxAmount = null);
}
