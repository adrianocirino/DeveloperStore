using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById;

/// <summary>
/// Handler for the GetSaleByIdQuery.
/// </summary>
public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, OneOf<SaleResponse, NotFoundError>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the GetSaleByIdQueryHandler class.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSaleByIdQuery.
    /// </summary>
    /// <param name="request">The get sale by ID query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<OneOf<SaleResponse, NotFoundError>> Handle(
        GetSaleByIdQuery request,
        CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id);

        if (sale == null)
        {
            return new NotFoundError($"Sale with ID '{request.Id}' was not found.");
        }

        var response = _mapper.Map<SaleResponse>(sale);
        return response;
    }
}
