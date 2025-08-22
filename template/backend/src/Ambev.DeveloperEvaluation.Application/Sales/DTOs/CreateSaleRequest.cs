using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Application.Sales.DTOs;

/// <summary>
/// DTO for creating a new sale.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer information.
    /// </summary>
    [Required]
    public CustomerDto Customer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the branch information.
    /// </summary>
    [Required]
    public BranchDto Branch { get; set; } = null!;

    /// <summary>
    /// Gets or sets the collection of sale items.
    /// </summary>
    [Required]
    [MinLength(1, ErrorMessage = "At least one item is required.")]
    public List<SaleItemDto> Items { get; set; } = new();
}

/// <summary>
/// DTO for customer information.
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// Gets or sets the customer's external ID.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string ExternalId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's name.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's email address.
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer's phone number.
    /// </summary>
    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
}

/// <summary>
/// DTO for branch information.
/// </summary>
public class BranchDto
{
    /// <summary>
    /// Gets or sets the branch's external ID.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string ExternalId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's name.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's address.
    /// </summary>
    [Required]
    [StringLength(500)]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's city.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch's state.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string State { get; set; } = string.Empty;
}

/// <summary>
/// DTO for sale item information.
/// </summary>
public class SaleItemDto
{
    /// <summary>
    /// Gets or sets the product information.
    /// </summary>
    [Required]
    public ProductDto Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the quantity of the product.
    /// </summary>
    [Required]
    [Range(1, 20, ErrorMessage = "Quantity must be between 1 and 20.")]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero.")]
    public decimal UnitPrice { get; set; }
}

/// <summary>
/// DTO for product information.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Gets or sets the product's external ID.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string ExternalId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's name.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's description.
    /// </summary>
    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's category.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product's brand.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Brand { get; set; } = string.Empty;
}
