using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using MediatR;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById;

/// <summary>
/// Query for getting a sale by its ID.
/// </summary>
public class GetSaleByIdQuery : IRequest<OneOf<SaleResponse, NotFoundError>>
{
    /// <summary>
    /// Gets or sets the sale ID.
    /// </summary>
    public Guid Id { get; set; }
}
