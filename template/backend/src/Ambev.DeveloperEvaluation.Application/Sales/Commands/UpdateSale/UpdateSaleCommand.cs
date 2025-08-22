using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using MediatR;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

/// <summary>
/// Command for updating an existing sale.
/// </summary>
public class UpdateSaleCommand : IRequest<OneOf<SaleResponse, NotFoundError, ValidationError, BusinessRuleError>>
{
    /// <summary>
    /// Gets or sets the sale ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the customer information.
    /// </summary>
    public CustomerDto Customer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the branch information.
    /// </summary>
    public BranchDto Branch { get; set; } = null!;
}
