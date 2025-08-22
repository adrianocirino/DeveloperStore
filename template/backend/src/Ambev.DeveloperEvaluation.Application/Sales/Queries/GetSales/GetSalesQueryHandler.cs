using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSales;

/// <summary>
/// Handler for the GetSalesQuery.
/// </summary>
public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, GetSalesResponse>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the GetSalesQueryHandler class.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetSalesQuery.
    /// </summary>
    /// <param name="request">The get sales query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<GetSalesResponse> Handle(
        GetSalesQuery request,
        CancellationToken cancellationToken)
    {
        // Get sales with filtering and pagination
        var sales = await _saleRepository.GetAllAsync(
            request.Page,
            request.Size,
            request.OrderBy,
            request.CustomerId,
            request.BranchId,
            request.Status,
            request.MinDate,
            request.MaxDate,
            request.MinAmount,
            request.MaxAmount);

        // Get total count for pagination
        var totalCount = await _saleRepository.GetCountAsync(
            request.CustomerId,
            request.BranchId,
            request.Status,
            request.MinDate,
            request.MaxDate,
            request.MinAmount,
            request.MaxAmount);

        // Calculate pagination info
        var totalPages = (int)Math.Ceiling((double)totalCount / request.Size);

        // Map to response DTOs
        var saleSummaries = _mapper.Map<List<SaleSummaryResponse>>(sales);

        return new GetSalesResponse
        {
            Sales = saleSummaries,
            TotalCount = totalCount,
            CurrentPage = request.Page,
            PageSize = request.Size,
            TotalPages = totalPages
        };
    }
}
