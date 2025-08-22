using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;
using MediatR;
using OneOf;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;

/// <summary>
/// Handler for the CreateSaleCommand.
/// </summary>
public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, OneOf<SaleResponse, ValidationError, BusinessRuleError>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the CreateSaleCommandHandler class.
    /// </summary>
    /// <param name="saleRepository">The sale repository.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateSaleCommandHandler(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateSaleCommand.
    /// </summary>
    /// <param name="request">The create sale command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the operation.</returns>
    public async Task<OneOf<SaleResponse, ValidationError, BusinessRuleError>> Handle(
        CreateSaleCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            // Check if sale number already exists
            if (await _saleRepository.SaleNumberExistsAsync(request.SaleNumber))
            {
                return new BusinessRuleError($"Sale number '{request.SaleNumber}' already exists.");
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

            // Create sale items
            var saleItems = new List<SaleItem>();
            foreach (var itemDto in request.Items)
            {
                var product = new Product(
                    itemDto.Product.ExternalId,
                    itemDto.Product.Name,
                    itemDto.Product.Description,
                    itemDto.Product.Category,
                    itemDto.Product.Brand);

                var saleItem = SaleItem.Create(product, itemDto.Quantity, itemDto.UnitPrice, Guid.Empty);
                saleItems.Add(saleItem);
            }

            // Create the sale
            var sale = Sale.Create(request.SaleNumber, customer, branch, saleItems);

            // Save to repository
            var savedSale = await _saleRepository.AddAsync(sale);

            // Map to response DTO
            var response = _mapper.Map<SaleResponse>(savedSale);

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
