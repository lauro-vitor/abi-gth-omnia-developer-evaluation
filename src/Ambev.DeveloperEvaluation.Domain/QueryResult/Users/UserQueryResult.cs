using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.QueryResult.Users;

/// <summary>
/// Represents the result of a user query, containing detailed information about the user.
/// This class is used to return user data in a structured format, typically for API responses or data transfers.
/// </summary>
public class UserQueryResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="UserQueryResult"/> class using data from a <see cref="User"/> object.
    /// </summary>
    public UserQueryResult(){ }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserQueryResult"/> class using data from a <see cref="User"/> object.
    /// </summary>
    /// <param name="user">The <see cref="User"/> object containing the data to be mapped.</param>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="user"/> parameter is null.</exception>
    public UserQueryResult(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User cannot be null.");

        Id = user.Id;
        Email = user.Email;
        Username = user.Username;
        Password = user.Password;
        Phone = user.Phone;
        Status = user.Status.ToString();
        Role = user.Role.ToString(); 

        Name = new NameQueryResult
        {
            Firstname = user.Firstname,
            Lastname = user.Lastname,
        };

        Address = new AddressQueryResult
        {
            City = user.City,
            Street = user.Street,
            Number = user.Number,
            Zipcode = user.Zipcode,
            Geolocation = new GeolocationQueryResult
            {
                Lat = user.GeolocationLat,
                Long = user.GeolocationLong
            }
        };
    }
}



