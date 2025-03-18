using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser
{
    //// <summary>
    /// Validator for <see cref="UpdateUserRequest"/> that defines validation rules for updating a user.
    /// </summary>
    /// <remarks>
    /// This validator ensures that the data provided in the <see cref="UpdateUserRequest"/> is valid
    /// and follows the required business rules. The validation rules include:
    /// <list type="bullet">
    ///     <item><description><b>Email</b>: Must be in a valid format (using <see cref="EmailValidator"/>).</description></item>
    ///     <item><description><b>Username</b>: Required, must be between 3 and 50 characters.</description></item>
    ///     <item><description><b>Password</b>: Must meet security requirements (using <see cref="PasswordValidator"/>).</description></item>
    ///     <item><description><b>Phone</b>: Must match the international phone format (+X XXXXXXXXXX).</description></item>
    ///     <item><description><b>Status</b>: Cannot be set to <see cref="UserStatus.Unknown"/>.</description></item>
    ///     <item><description><b>Role</b>: Cannot be set to <see cref="UserRole.None"/>.</description></item>
    /// </list>
    /// </remarks>
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserRequestValidator"/> with defined validation rules.
        /// </summary>
        public UpdateUserRequestValidator()
        {
            RuleFor(user => user.Email).SetValidator(new EmailValidator());
            RuleFor(user => user.Username).NotEmpty().Length(3, 50);
            RuleFor(user => user.Password).SetValidator(new PasswordValidator());
            RuleFor(user => user.Phone).Matches(@"^\+?[1-9]\d{1,14}$");
            RuleFor(user => user.Status).NotEqual(UserStatus.Unknown);
            RuleFor(user => user.Role).NotEqual(UserRole.None);
        }
    }
}
