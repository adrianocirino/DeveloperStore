using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using MediatR;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;

/// <summary>
/// Command for creating a new sale.
/// </summary>
public class CreateSaleCommand : IRequest<OneOf<SaleResponse, ValidationError, BusinessRuleError>>
{
    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer information.
    /// </summary>
    public CustomerDto Customer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the branch information.
    /// </summary>
    public BranchDto Branch { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of sale items.
    /// </summary>
    public List<SaleItemDto> Items { get; set; } = new();
}
