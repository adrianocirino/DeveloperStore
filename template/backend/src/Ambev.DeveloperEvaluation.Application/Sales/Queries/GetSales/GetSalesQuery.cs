using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSales;

/// <summary>
/// Query for getting a list of sales with pagination and filtering.
/// </summary>
public class GetSalesQuery : IRequest<GetSalesResponse>
{
    /// <summary>
    /// Gets or sets the page number.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// Gets or sets the ordering criteria.
    /// </summary>
    public string? OrderBy { get; set; }

    /// <summary>
    /// Gets or sets the customer ID filter.
    /// </summary>
    public string? CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the branch ID filter.
    /// </summary>
    public string? BranchId { get; set; }

    /// <summary>
    /// Gets or sets the status filter.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the minimum date filter.
    /// </summary>
    public DateTime? MinDate { get; set; }

    /// <summary>
    /// Gets or sets the maximum date filter.
    /// </summary>
    public DateTime? MaxDate { get; set; }

    /// <summary>
    /// Gets or sets the minimum amount filter.
    /// </summary>
    public decimal? MinAmount { get; set; }

    /// <summary>
    /// Gets or sets the maximum amount filter.
    /// </summary>
    public decimal? MaxAmount { get; set; }
}

/// <summary>
/// Response for the GetSalesQuery.
/// </summary>
public class GetSalesResponse
{
    /// <summary>
    /// Gets or sets the collection of sales.
    /// </summary>
    public List<SaleSummaryResponse> Sales { get; set; } = new();

    /// <summary>
    /// Gets or sets the total count of sales.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Gets or sets the current page.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the page size.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the total pages.
    /// </summary>
    public int TotalPages { get; set; }
}

/// <summary>
/// Summary response for a sale.
/// </summary>
public class SaleSummaryResponse
{
    /// <summary>
    /// Gets or sets the sale ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sale date.
    /// </summary>
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the customer name.
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total sale amount.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the branch name.
    /// </summary>
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sale status.
    /// </summary>
    public string Status { get; set; } = string.Empty;
}
