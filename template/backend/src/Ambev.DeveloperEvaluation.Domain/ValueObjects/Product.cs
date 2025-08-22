namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents product information as a value object.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets the product's external ID from the product domain.
    /// </summary>
    public string ExternalId { get; }

    /// <summary>
    /// Gets the product's name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the product's description.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the product's category.
    /// </summary>
    public string Category { get; }

    /// <summary>
    /// Gets the product's brand.
    /// </summary>
    public string Brand { get; }

    /// <summary>
    /// Initializes a new instance of the Product class.
    /// </summary>
    /// <param name="externalId">The product's external ID.</param>
    /// <param name="name">The product's name.</param>
    /// <param name="description">The product's description.</param>
    /// <param name="category">The product's category.</param>
    /// <param name="brand">The product's brand.</param>
    public Product(string externalId, string name, string description, string category, string brand)
    {
        if (string.IsNullOrWhiteSpace(externalId))
            throw new ArgumentException("External ID cannot be null or empty.", nameof(externalId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be null or empty.", nameof(description));

        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Category cannot be null or empty.", nameof(category));

        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Brand cannot be null or empty.", nameof(brand));

        ExternalId = externalId;
        Name = name;
        Description = description;
        Category = category;
        Brand = brand;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Product other)
            return false;

        return ExternalId == other.ExternalId &&
               Name == other.Name &&
               Description == other.Description &&
               Category == other.Category &&
               Brand == other.Brand;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ExternalId, Name, Description, Category, Brand);
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"Product: {Name} - {Brand} ({Category})";
    }
}
