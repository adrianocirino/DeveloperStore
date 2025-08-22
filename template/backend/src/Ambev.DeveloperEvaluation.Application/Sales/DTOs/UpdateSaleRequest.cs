using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs;

/// <summary>
/// DTO for updating an existing sale.
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// Gets or sets the customer information.
    /// </summary>
    [Required]
    public CustomerDto Customer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the branch information.
    /// </summary>
    [Required]
    public BranchDto Branch { get; set; } = null!;
}
