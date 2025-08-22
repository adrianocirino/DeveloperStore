using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.ORM.Entities;

/// <summary>
/// Database entity for sales.
/// </summary>
[Table("sales")]
public class SaleEntity
{
    /// <summary>
    /// Gets or sets the sale ID.
    /// </summary>
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number.
    /// </summary>
    [Required]
    [Column("sale_number")]
    [StringLength(50)]
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sale date.
    /// </summary>
    [Required]
    [Column("sale_date")]
    public DateTime SaleDate { get; set; }

    /// <summary>
    /// Gets or sets the customer external ID.
    /// </summary>
    [Required]
    [Column("customer_external_id")]
    [StringLength(100)]
    public string CustomerExternalId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer name.
    /// </summary>
    [Required]
    [Column("customer_name")]
    [StringLength(200)]
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer email.
    /// </summary>
    [Required]
    [Column("customer_email")]
    [StringLength(200)]
    public string CustomerEmail { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer phone.
    /// </summary>
    [Required]
    [Column("customer_phone")]
    [StringLength(20)]
    public string CustomerPhone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the total sale amount.
    /// </summary>
    [Required]
    [Column("total_amount", TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the branch external ID.
    /// </summary>
    [Required]
    [Column("branch_external_id")]
    [StringLength(100)]
    public string BranchExternalId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch name.
    /// </summary>
    [Required]
    [Column("branch_name")]
    [StringLength(200)]
    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch address.
    /// </summary>
    [Required]
    [Column("branch_address")]
    [StringLength(500)]
    public string BranchAddress { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch city.
    /// </summary>
    [Required]
    [Column("branch_city")]
    [StringLength(100)]
    public string BranchCity { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch state.
    /// </summary>
    [Required]
    [Column("branch_state")]
    [StringLength(100)]
    public string BranchState { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sale status.
    /// </summary>
    [Required]
    [Column("status")]
    [StringLength(20)]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation date.
    /// </summary>
    [Required]
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the last update date.
    /// </summary>
    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the collection of sale items.
    /// </summary>
    public virtual ICollection<SaleItemEntity> Items { get; set; } = new List<SaleItemEntity>();
}
