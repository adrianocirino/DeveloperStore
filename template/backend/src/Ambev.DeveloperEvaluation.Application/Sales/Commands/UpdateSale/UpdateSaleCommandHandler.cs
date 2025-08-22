using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

/// <summary>
/// Handler for the UpdateSaleCommand.
/// </summary>
public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, OneOf<SaleResponse, NotFoundError, ValidationError, BusinessRuleError>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the UpdateSaleCommandHandler class.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public UpdateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand.
    /// </summary>
    /// <param name="request">The update sale command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<OneOf<SaleResponse, NotFoundError, ValidationError, BusinessRuleError>> Handle(
        UpdateSaleCommand request,
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

            // Create value objects
            var customer = new Customer(
                request.Customer.ExternalId,
                request.Customer.Name,
                request.Customer.Email,
                request.Customer.Phone);

            var branch = new Branch(
                request.Branch.ExternalId,
                request.Branch.Name,
                request.Branch.Address,
                request.Branch.City,
                request.Branch.State);

            // Update the sale
            existingSale.Update(customer, branch);

            // Save to repository
            var updatedSale = await _saleRepository.UpdateAsync(existingSale);

            // Map to response DTO
            var response = _mapper.Map<SaleResponse>(updatedSale);

            return response;
        }
        catch (ArgumentException ex)
        {
            return new ValidationError($"Validation error: {ex.Message}");
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
