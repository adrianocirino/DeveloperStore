using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;

/// <summary>
/// Handler for the CancelSaleCommand.
/// </summary>
public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, OneOf<SuccessResponse, NotFoundError, BusinessRuleError>>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of the CancelSaleCommandHandler class.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    public CancelSaleCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the CancelSaleCommand.
    /// </summary>
    /// <param name="request">The cancel sale command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<OneOf<SuccessResponse, NotFoundError, BusinessRuleError>> Handle(
        CancelSaleCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Get the existing sale
            var existingSale = await _saleRepository.GetByIdAsync(request.Id);

            if (existingSale == null)
            {
                return new NotFoundError($"Sale with ID '{request.Id}' was not found.");
            }

            // Cancel the sale
            existingSale.Cancel();

            // Save to repository
            await _saleRepository.UpdateAsync(existingSale);

            return new SuccessResponse($"Sale '{existingSale.SaleNumber}' has been cancelled successfully.");
        }
        catch (InvalidOperationException ex)
        {
            return new BusinessRuleError($"Business rule violation: {ex.Message}");
        }
        catch (Exception ex)
        {
            return new BusinessRuleError($"An unexpected error occurred: {ex.Message}");
        }
    }
}
