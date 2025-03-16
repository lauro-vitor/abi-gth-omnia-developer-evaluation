using Ambev.DeveloperEvaluation.Domain.QueryResult.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Represents the response returned after successfully creating a new user.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the newly created user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class CreateUserResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the newly created user.
    /// </summary>
    /// <value>A GUID that uniquely identifies the created user in the system.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the user's email address.
    /// Must be a valid email format and is used as a unique identifier for authentication.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's username.
    /// Must not be null or empty and should be unique within the system.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's password.
    /// This field is typically used for input and should be hashed before storage.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's full name, including first and last names.
    /// </summary>
    public NameQueryResult Name { get; set; } = new NameQueryResult();

    /// <summary>
    /// Gets or sets the user's address, including city, street, number, zip code, and geolocation.
    /// </summary>
    public AddressQueryResult Address { get; set; } = new AddressQueryResult();

    /// <summary>
    /// Gets or sets the user's phone number.
    /// Must be a valid phone number format following the pattern (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's current status.
    /// Indicates whether the user is active, inactive, or suspended in the system.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's role in the system.
    /// Determines the user's permissions and access levels.
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
