namespace Ambev.DeveloperEvaluation.Application.Sales.Common;

/// <summary>
/// Represents a not found error.
/// </summary>
public record NotFoundError(string Message);

/// <summary>
/// Represents a validation error.
/// </summary>
public record ValidationError(string Message);

/// <summary>
/// Represents a business rule error.
/// </summary>
public record BusinessRuleError(string Message);

/// <summary>
/// Represents a successful operation response.
/// </summary>
public record SuccessResponse(string Message);
