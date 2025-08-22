namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents customer information as a value object.
/// </summary>
public class Customer
{
    /// <summary>
    /// Gets the customer's external ID from the customer domain.
    /// </summary>
    public string ExternalId { get; }

    /// <summary>
    /// Gets the customer's name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the customer's email address.
    /// </summary>
    public string Email { get; }

    /// <summary>
    /// Gets the customer's phone number.
    /// </summary>
    public string Phone { get; }

    /// <summary>
    /// Initializes a new instance of the Customer class.
    /// </summary>
    /// <param name="externalId">The customer's external ID.</param>
    /// <param name="name">The customer's name.</param>
    /// <param name="email">The customer's email address.</param>
    /// <param name="phone">The customer's phone number.</param>
    public Customer(string externalId, string name, string email, string phone)
    {
        if (string.IsNullOrWhiteSpace(externalId))
            throw new ArgumentException("External ID cannot be null or empty.", nameof(externalId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone cannot be null or empty.", nameof(phone));

        ExternalId = externalId;
        Name = name;
        Email = email;
        Phone = phone;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Customer other)
            return false;

        return ExternalId == other.ExternalId &&
               Name == other.Name &&
               Email == other.Email &&
               Phone == other.Phone;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ExternalId, Name, Email, Phone);
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"Customer: {Name} ({Email})";
    }
}
