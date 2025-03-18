using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.QueryResult.Users;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Command for updating an existing user.
    /// </summary>
    /// <remarks>
    /// This command is used to capture the required data for updating a user, 
    /// including username, password, phone number, email, status, and role. 
    /// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
    /// that returns a <see cref="UpdateUserResult"/>.
    /// 
    /// The data provided in this command is validated using the 
    /// <see cref="UpdateUserValidator"/>, which extends 
    /// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
    /// populated and follow the required rules.
    /// </remarks>
    public class UpdateUserCommand : IRequest<UpdateUserResult>
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user to be updated.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user's name information, including first and last names.
        /// This property is of type <see cref="NameQueryResult"/>, which encapsulates
        /// the user's full name details.
        /// </summary>
        public NameQueryResult Name { get; set; } = new NameQueryResult();

        /// <summary>
        /// Gets or sets the user's address information, including city, street, number,
        /// zip code, and geolocation.
        /// This property is of type <see cref="AddressQueryResult"/>, which encapsulates
        /// the user's complete address details.
        /// </summary>
        public AddressQueryResult Address { get; set; } = new AddressQueryResult();

        /// <summary>
        /// Gets or sets the phone number for the user.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address for the user.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the user.
        /// </summary>
        public UserStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public UserRole Role { get; set; }

        /// <summary>
        /// Validates the command data using the <see cref="UpdateUserValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing:
        /// - IsValid: Indicates whether all validation rules passed.
        /// - Errors: Collection of validation errors if any rules failed.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new UpdateUserValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
