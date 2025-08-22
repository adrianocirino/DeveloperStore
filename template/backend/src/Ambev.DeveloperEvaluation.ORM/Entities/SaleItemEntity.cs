using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.ORM.Entities;

/// <summary>
/// Database entity for sale items.
/// </summary>
[Table("sale_items")]
public class SaleItemEntity
{
    /// <summary>
    /// Gets or sets the item ID.
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale ID.
    /// </summary>
    [Required]
    [Column("sale_id")]
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the product external ID.
    /// </summary>
    [Required]
    [Column("product_external_id")]
    [StringLength(100)]
    public string ProductExternalId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    [Required]
    [Column("product_name")]
    [StringLength(200)]
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product description.
    /// </summary>
    [Required]
    [Column("product_description")]
    [StringLength(1000)]
    public string ProductDescription { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product category.
    /// </summary>
    [Required]
    [Column("product_category")]
    [StringLength(100)]
    public string ProductCategory { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product brand.
    /// </summary>
    [Required]
    [Column("product_brand")]
    [StringLength(100)]
    public string ProductBrand { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    [Required]
    [Column("quantity")]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price.
    /// </summary>
    [Required]
    [Column("unit_price", TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage.
    /// </summary>
    [Required]
    [Column("discount_percentage", TypeName = "decimal(5,2)")]
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// Gets or sets the total amount.
    /// </summary>
    [Required]
    [Column("total_amount", TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the sale entity.
    /// </summary>
    [ForeignKey("SaleId")]
    public virtual SaleEntity Sale { get; set; } = null!;
}
