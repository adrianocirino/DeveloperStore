using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.ORM.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of the sale repository.
/// </summary>
public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the SaleRepository class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<Sale?> GetByIdAsync(Guid id)
    {
        var saleEntity = await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (saleEntity == null)
            return null;

        return MapToDomain(saleEntity);
    }

    /// <inheritdoc/>
    public async Task<Sale?> GetBySaleNumberAsync(string saleNumber)
    {
        var saleEntity = await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber);

        if (saleEntity == null)
            return null;

        return MapToDomain(saleEntity);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Sale>> GetAllAsync(
        int page = 1,
        int size = 10,
        string? orderBy = null,
        string? customerId = null,
        string? branchId = null,
        string? status = null,
        DateTime? minDate = null,
        DateTime? maxDate = null,
        decimal? minAmount = null,
        decimal? maxAmount = null)
    {
        var query = _context.Sales.AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(customerId))
            query = query.Where(s => s.CustomerExternalId == customerId);

        if (!string.IsNullOrEmpty(branchId))
            query = query.Where(s => s.BranchExternalId == branchId);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(s => s.Status == status);

        if (minDate.HasValue)
            query = query.Where(s => s.SaleDate >= minDate.Value);

        if (maxDate.HasValue)
            query = query.Where(s => s.SaleDate <= maxDate.Value);

        if (minAmount.HasValue)
            query = query.Where(s => s.TotalAmount >= minAmount.Value);

        if (maxAmount.HasValue)
            query = query.Where(s => s.TotalAmount <= maxAmount.Value);

        // Apply ordering
        if (!string.IsNullOrEmpty(orderBy))
        {
            var orderParts = orderBy.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in orderParts)
            {
                var trimmedPart = part.Trim();
                if (trimmedPart.StartsWith("saleDate", StringComparison.OrdinalIgnoreCase))
                {
                    query = trimmedPart.EndsWith("desc", StringComparison.OrdinalIgnoreCase)
                        ? query.OrderByDescending(s => s.SaleDate)
                        : query.OrderBy(s => s.SaleDate);
                }
                else if (trimmedPart.StartsWith("totalAmount", StringComparison.OrdinalIgnoreCase))
                {
                    query = trimmedPart.EndsWith("desc", StringComparison.OrdinalIgnoreCase)
                        ? query.OrderByDescending(s => s.TotalAmount)
                        : query.OrderBy(s => s.TotalAmount);
                }
                else if (trimmedPart.StartsWith("createdAt", StringComparison.OrdinalIgnoreCase))
                {
                    query = trimmedPart.EndsWith("desc", StringComparison.OrdinalIgnoreCase)
                        ? query.OrderByDescending(s => s.CreatedAt)
                        : query.OrderBy(s => s.CreatedAt);
                }
            }
        }
        else
        {
            // Default ordering
            query = query.OrderByDescending(s => s.CreatedAt);
        }

        // Apply pagination
        var sales = await query
            .Skip((page - 1) * size)
            .Take(size)
            .Include(s => s.Items)
            .ToListAsync();

        return sales.Select(MapToDomain);
    }

    /// <inheritdoc/>
    public async Task<Sale> AddAsync(Sale sale)
    {
        var saleEntity = MapToEntity(sale);
        _context.Sales.Add(saleEntity);
        await _context.SaveChangesAsync();

        // Reload the entity to get the generated ID
        await _context.Entry(saleEntity).ReloadAsync();
        return MapToDomain(saleEntity);
    }

    /// <inheritdoc/>
    public async Task<Sale> UpdateAsync(Sale sale)
    {
        var existingEntity = await _context.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(s => s.Id == sale.Id);

        if (existingEntity == null)
            throw new InvalidOperationException($"Sale with ID '{sale.Id}' not found.");

        // Update the entity
        UpdateEntity(existingEntity, sale);
        await _context.SaveChangesAsync();

        return MapToDomain(existingEntity);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id)
    {
        var saleEntity = await _context.Sales.FindAsync(id);
        if (saleEntity == null)
            return false;

        _context.Sales.Remove(saleEntity);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<bool> SaleNumberExistsAsync(string saleNumber)
    {
        return await _context.Sales.AnyAsync(s => s.SaleNumber == saleNumber);
    }

    /// <inheritdoc/>
    public async Task<int> GetCountAsync(
        string? customerId = null,
        string? branchId = null,
        string? status = null,
        DateTime? minDate = null,
        DateTime? maxDate = null,
        decimal? minAmount = null,
        decimal? maxAmount = null)
    {
        var query = _context.Sales.AsQueryable();

        // Apply filters
        if (!string.IsNullOrEmpty(customerId))
            query = query.Where(s => s.CustomerExternalId == customerId);

        if (!string.IsNullOrEmpty(branchId))
            query = query.Where(s => s.BranchExternalId == branchId);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(s => s.Status == status);

        if (minDate.HasValue)
            query = query.Where(s => s.SaleDate >= minDate.Value);

        if (maxDate.HasValue)
            query = query.Where(s => s.SaleDate <= maxDate.Value);

        if (minAmount.HasValue)
            query = query.Where(s => s.TotalAmount >= minAmount.Value);

        if (maxAmount.HasValue)
            query = query.Where(s => s.TotalAmount <= maxAmount.Value);

        return await query.CountAsync();
    }

    private static SaleEntity MapToEntity(Sale sale)
    {
        var saleEntity = new SaleEntity
        {
            Id = sale.Id,
            SaleNumber = sale.SaleNumber,
            SaleDate = sale.SaleDate,
            CustomerExternalId = sale.Customer.ExternalId,
            CustomerName = sale.Customer.Name,
            CustomerEmail = sale.Customer.Email,
            CustomerPhone = sale.Customer.Phone,
            TotalAmount = sale.TotalAmount,
            BranchExternalId = sale.Branch.ExternalId,
            BranchName = sale.Branch.Name,
            BranchAddress = sale.Branch.Address,
            BranchCity = sale.Branch.City,
            BranchState = sale.Branch.State,
            Status = sale.Status.ToString(),
            CreatedAt = sale.CreatedAt,
            UpdatedAt = sale.UpdatedAt
        };

        // Map items
        foreach (var item in sale.Items)
        {
            var itemEntity = new SaleItemEntity
            {
                Id = item.Id,
                SaleId = sale.Id,
                ProductExternalId = item.Product.ExternalId,
                ProductName = item.Product.Name,
                ProductDescription = item.Product.Description,
                ProductCategory = item.Product.Category,
                ProductBrand = item.Product.Brand,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                DiscountPercentage = item.DiscountPercentage,
                TotalAmount = item.TotalAmount
            };

            saleEntity.Items.Add(itemEntity);
        }

        return saleEntity;
    }

    private static Sale MapToDomain(SaleEntity entity)
    {
        // Create value objects
        var customer = new Customer(
            entity.CustomerExternalId,
            entity.CustomerName,
            entity.CustomerEmail,
            entity.CustomerPhone);

        var branch = new Branch(
            entity.BranchExternalId,
            entity.BranchName,
            entity.BranchAddress,
            entity.BranchCity,
            entity.BranchState);

        // Create sale items
        var items = entity.Items.Select(item => SaleItem.Create(
            new Product(
                item.ProductExternalId,
                item.ProductName,
                item.ProductDescription,
                item.ProductCategory,
                item.ProductBrand),
            item.Quantity,
            item.UnitPrice,
            entity.Id)).ToList();

        // Create the sale
        var sale = Sale.Create(entity.SaleNumber, customer, branch, items);
        
        // Set the ID and other properties
        var saleType = typeof(Sale);
        var idProperty = saleType.GetProperty("Id");
        idProperty?.SetValue(sale, entity.Id);

        var saleDateProperty = saleType.GetProperty("SaleDate");
        saleDateProperty?.SetValue(sale, entity.SaleDate);

        var createdAtProperty = saleType.GetProperty("CreatedAt");
        createdAtProperty?.SetValue(sale, entity.CreatedAt);

        var updatedAtProperty = saleType.GetProperty("UpdatedAt");
        updatedAtProperty?.SetValue(sale, entity.UpdatedAt);

        // Set the status
        if (Enum.TryParse<Domain.Enums.SaleStatus>(entity.Status, out var status))
        {
            var statusProperty = saleType.GetProperty("Status");
            statusProperty?.SetValue(sale, status);
        }

        return sale;
    }

    private static void UpdateEntity(SaleEntity entity, Sale sale)
    {
        entity.CustomerExternalId = sale.Customer.ExternalId;
        entity.CustomerName = sale.Customer.Name;
        entity.CustomerEmail = sale.Customer.Email;
        entity.CustomerPhone = sale.Customer.Phone;
        entity.BranchExternalId = sale.Branch.ExternalId;
        entity.BranchName = sale.Branch.Name;
        entity.BranchAddress = sale.Branch.Address;
        entity.BranchCity = sale.Branch.City;
        entity.BranchState = sale.Branch.State;
        entity.Status = sale.Status.ToString();
        entity.UpdatedAt = sale.UpdatedAt;
        entity.TotalAmount = sale.TotalAmount;

        // Update items
        entity.Items.Clear();
        foreach (var item in sale.Items)
        {
            var itemEntity = new SaleItemEntity
            {
                Id = item.Id,
                SaleId = sale.Id,
                ProductExternalId = item.Product.ExternalId,
                ProductName = item.Product.Name,
                ProductDescription = item.Product.Description,
                ProductCategory = item.Product.Category,
                ProductBrand = item.Product.Brand,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                DiscountPercentage = item.DiscountPercentage,
                TotalAmount = item.TotalAmount
            };

            entity.Items.Add(itemEntity);
        }
    }
}
