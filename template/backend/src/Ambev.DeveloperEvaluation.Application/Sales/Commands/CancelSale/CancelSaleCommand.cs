using Ambev.DeveloperEvaluation.Application.Sales.Common;
using MediatR;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;

/// <summary>
/// Command for cancelling an existing sale.
/// </summary>
public class CancelSaleCommand : IRequest<OneOf<SuccessResponse, NotFoundError, BusinessRuleError>>
{
    /// <summary>
    /// Gets or sets the sale ID.
    /// </summary>
    public Guid Id { get; set; }
}
