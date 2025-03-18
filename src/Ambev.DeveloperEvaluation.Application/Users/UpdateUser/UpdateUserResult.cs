using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.QueryResult.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Represents the response returned after successfully updating an existing user.
    /// </summary>
    /// <remarks>
    /// This response contains the updated details of the user, including their unique identifier,
    /// username, email, password, name, address, phone number, status, and role.
    /// </remarks>
    public class UpdateUserResult
    {
        /// <summary>
        /// Gets or sets the unique identifier of the updated user.
        /// </summary>
        /// <value>A GUID that uniquely identifies the user in the system.</value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        /// <value>A valid email address used for authentication and communication.</value>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's username.
        /// </summary>
        /// <value>A unique identifier for the user within the system.</value>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        /// <value>The user's password. This field is typically used for input and should be hashed before storage.</value>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's full name, including first and last names.
        /// </summary>
        /// <value>An instance of <see cref="NameQueryResult"/> containing the user's name details.</value>
        public NameQueryResult Name { get; set; } = new NameQueryResult();

        /// <summary>
        /// Gets or sets the user's address, including city, street, number, zip code, and geolocation.
        /// </summary>
        /// <value>An instance of <see cref="AddressQueryResult"/> containing the user's address details.</value>
        public AddressQueryResult Address { get; set; } = new AddressQueryResult();

        /// <summary>
        /// Gets or sets the user's phone number.
        /// </summary>
        /// <value>A valid phone number following the format (XX) XXXXX-XXXX.</value>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's current status.
        /// </summary>
        /// <value>The status of the user, indicating whether they are active, inactive, or suspended.</value>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the user's role in the system.
        /// </summary>
        /// <value>The role of the user, determining their permissions and access levels.</value>
        public UserRole Role { get; set; }
    }
}
