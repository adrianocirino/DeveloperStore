using Ambev.DeveloperEvaluation.Application.Sales.Common;
using MediatR;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;

/// <summary>
/// Command for deleting an existing sale.
/// </summary>
public class DeleteSaleCommand : IRequest<OneOf<SuccessResponse, NotFoundError, BusinessRuleError>>
{
    /// <summary>
    /// Gets or set the sale ID.
    /// </summary>
    public Guid Id { get; set; }
}
