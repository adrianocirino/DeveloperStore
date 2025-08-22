namespace Ambev.DeveloperEvaluation.Domain.ValueObjects;

/// <summary>
/// Represents branch information as a value object.
/// </summary>
public class Branch
{
    /// <summary>
    /// Gets the branch's external ID from the branch domain.
    /// </summary>
    public string ExternalId { get; }

    /// <summary>
    /// Gets the branch's name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the branch's address.
    /// </summary>
    public string Address { get; }

    /// <summary>
    /// Gets the branch's city.
    /// </summary>
    public string City { get; }

    /// <summary>
    /// Gets the branch's state.
    /// </summary>
    public string State { get; }

    /// <summary>
    /// Initializes a new instance of the Branch class.
    /// </summary>
    /// <param name="externalId">The branch's external ID.</param>
    /// <param name="name">The branch's name.</param>
    /// <param name="address">The branch's address.</param>
    /// <param name="city">The branch's city.</param>
    /// <param name="state">The branch's state.</param>
    public Branch(string externalId, string name, string address, string city, string state)
    {
        if (string.IsNullOrWhiteSpace(externalId))
            throw new ArgumentException("External ID cannot be null or empty.", nameof(externalId));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be null or empty.", nameof(address));

        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City cannot be null or empty.", nameof(city));

        if (string.IsNullOrWhiteSpace(state))
            throw new ArgumentException("State cannot be null or empty.", nameof(state));

        ExternalId = externalId;
        Name = name;
        Address = address;
        City = city;
        State = state;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Branch other)
            return false;

        return ExternalId == other.ExternalId &&
               Name == other.Name &&
               Address == other.Address &&
               City == other.City &&
               State == other.State;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(ExternalId, Name, Address, City, State);
    }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"Branch: {Name} - {City}, {State}";
    }
}
