using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.QueryResult.Users;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
    /// <summary>
    /// API request model for UpdateUser operation
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// Gets or sets the username. Must be unique and contain only valid characters.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password. Must meet security requirements.
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
        /// Gets or sets the phone number in format (XX) XXXXX-XXXX.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address. Must be a valid email format.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the initial status of the user account.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the role assigned to the user.
        /// </summary>
        public UserRole Role { get; set; }
    }
}
