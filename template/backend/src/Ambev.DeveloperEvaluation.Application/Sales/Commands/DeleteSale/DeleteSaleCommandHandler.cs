using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;

/// <summary>
/// Handler for the DeleteSaleCommand.
/// </summary>
public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, OneOf<SuccessResponse, NotFoundError, BusinessRuleError>>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of the DeleteSaleCommandHandler class.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    public DeleteSaleCommandHandler(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    /// <summary>
    /// Handles the DeleteSaleCommand.
    /// </summary>
    /// <param name="request">The delete sale command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<OneOf<SuccessResponse, NotFoundError, BusinessRuleError>> Handle(
        DeleteSaleCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Get the existing sale to check if it exists
            var existingSale = await _saleRepository.GetByIdAsync(request.Id);

            if (existingSale == null)
            {
                return new NotFoundError($"Sale with ID '{request.Id}' was not found.");
            }

            // Delete the sale
            var deleted = await _saleRepository.DeleteAsync(request.Id);

            if (!deleted)
            {
                return new BusinessRuleError($"Failed to delete sale with ID '{request.Id}'.");
            }

            return new SuccessResponse($"Sale '{existingSale.SaleNumber}' has been deleted successfully.");
        }
        catch (Exception ex)
        {
            return new BusinessRuleError($"An unexpected error occurred: {ex.Message}");
        }
    }
}
