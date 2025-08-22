using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Common;
using Ambev.DeveloperEvaluation.Application.Sales.DTOs;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSales;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sales operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of the SalesController class.
    /// </summary>
    /// <param name="mediator">The MediatR mediator instance.</param>
    public SalesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets a list of sales with pagination and filtering.
    /// </summary>
    /// <param name="page">The page number (default: 1).</param>
    /// <param name="size">The page size (default: 10).</param>
    /// <param name="orderBy">The ordering criteria.</param>
    /// <param name="customerId">Optional customer ID filter.</param>
    /// <param name="branchId">Optional branch ID filter.</param>
    /// <param name="status">Optional status filter.</param>
    /// <param name="minDate">Optional minimum date filter.</param>
    /// <param name="maxDate">Optional maximum date filter.</param>
    /// <param name="minAmount">Optional minimum amount filter.</param>
    /// <param name="maxAmount">Optional maximum amount filter.</param>
    /// <returns>A paginated list of sales.</returns>
    /// <response code="200">Returns the list of sales.</response>
    /// <response code="400">If the request parameters are invalid.</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetSalesResponse), 200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<GetSalesResponse>> GetSales(
        [FromQuery] int page = 1,
        [FromQuery] int size = 10,
        [FromQuery] string? orderBy = null,
        [FromQuery] string? customerId = null,
        [FromQuery] string? branchId = null,
        [FromQuery] string? status = null,
        [FromQuery] DateTime? minDate = null,
        [FromQuery] DateTime? maxDate = null,
        [FromQuery] decimal? minAmount = null,
        [FromQuery] decimal? maxAmount = null)
    {
        var query = new GetSalesQuery
        {
            Page = page,
            Size = size,
            OrderBy = orderBy,
            CustomerId = customerId,
            BranchId = branchId,
            Status = status,
            MinDate = minDate,
            MaxDate = maxDate,
            MinAmount = minAmount,
            MaxAmount = maxAmount
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }

    /// <summary>
    /// Gets a sale by its ID.
    /// </summary>
    /// <param name="id">The sale ID.</param>
    /// <returns>The sale information.</returns>
    /// <response code="200">Returns the sale information.</response>
    /// <response code="404">If the sale was not found.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SaleResponse), 200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<SaleResponse>> GetSaleById(Guid id)
    {
        var query = new GetSaleByIdQuery { Id = id };
        var result = await _mediator.Send(query);

        return result.Match<ActionResult<SaleResponse>>(
            sale => Ok(sale),
            notFound => NotFound(new { error = notFound.Message })
        );
    }

    /// <summary>
    /// Creates a new sale.
    /// </summary>
    /// <param name="request">The sale creation request.</param>
    /// <returns>The created sale information.</returns>
    /// <response code="201">Returns the created sale.</response>
    /// <response code="400">If the request data is invalid.</response>
    /// <response code="409">If the sale number already exists.</response>
    [HttpPost]
    [ProducesResponseType(typeof(SaleResponse), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<ActionResult<SaleResponse>> CreateSale([FromBody] CreateSaleRequest request)
    {
        var command = new CreateSaleCommand
        {
            SaleNumber = request.SaleNumber,
            Customer = request.Customer,
            Branch = request.Branch,
            Items = request.Items
        };

        var result = await _mediator.Send(command);

        return result.Match<ActionResult<SaleResponse>>(
            sale => CreatedAtAction(nameof(GetSaleById), new { id = sale.Id }, sale),
            validationError => BadRequest(new { error = validationError.Message }),
            businessRuleError => Conflict(new { error = businessRuleError.Message })
        );
    }

    /// <summary>
    /// Updates an existing sale.
    /// </summary>
    /// <param name="id">The sale ID.</param>
    /// <param name="request">The sale update request.</param>
    /// <returns>The updated sale information.</returns>
    /// <response code="200">Returns the updated sale.</response>
    /// <response code="400">If the request data is invalid.</response>
    /// <response code="404">If the sale was not found.</response>
    /// <response code="409">If the sale cannot be updated due to business rules.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SaleResponse), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    public async Task<ActionResult<SaleResponse>> UpdateSale(Guid id, [FromBody] UpdateSaleRequest request)
    {
        var command = new UpdateSaleCommand
        {
            Id = id,
            Customer = request.Customer,
            Branch = request.Branch
        };

        var result = await _mediator.Send(command);

        return result.Match<ActionResult<SaleResponse>>(
            sale => Ok(sale),
            notFound => NotFound(new { error = notFound.Message }),
            validationError => BadRequest(new { error = validationError.Message }),
            businessRuleError => Conflict(new { error = businessRuleError.Message })
        );
    }

    /// <summary>
    /// Cancels an existing sale.
    /// </summary>
    /// <param name="id">The sale ID.</param>
    /// <returns>A success message.</returns>
    /// <response code="200">Returns a success message.</response>
    /// <response code="404">If the sale was not found.</response>
    /// <response code="409">If the sale cannot be cancelled due to business rules.</response>
    [HttpPatch("{id:guid}/cancel")]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    public async Task<ActionResult<object>> CancelSale(Guid id)
    {
        var command = new CancelSaleCommand { Id = id };
        var result = await _mediator.Send(command);

        return result.Match<ActionResult<object>>(
            success => Ok(new { message = success.Message }),
            notFound => NotFound(new { error = notFound.Message }),
            businessRuleError => Conflict(new { error = businessRuleError.Message })
        );
    }

    /// <summary>
    /// Deletes an existing sale.
    /// </summary>
    /// <param name="id">The sale ID.</param>
    /// <returns>A success message.</returns>
    /// <response code="200">Returns a success message.</response>
    /// <response code="404">If the sale was not found.</response>
    /// <response code="409">If the sale cannot be deleted due to business rules.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    public async Task<ActionResult<object>> DeleteSale(Guid id)
    {
        var command = new DeleteSaleCommand { Id = id };
        var result = await _mediator.Send(command);

        return result.Match<ActionResult<object>>(
            success => Ok(new { message = success.Message }),
            notFound => NotFound(new { error = notFound.Message }),
            businessRuleError => Conflict(new { error = businessRuleError.Message })
        );
    }
}
