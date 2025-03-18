using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{

    /// <summary>
    /// Handler for processing <see cref="UpdateUserCommand"/> requests.
    /// </summary>
    /// <remarks>
    /// This handler is responsible for updating an existing user in the system.
    /// It performs the following steps:
    /// <list type="number">
    ///     <item><description>Validates the <see cref="UpdateUserCommand"/> using <see cref="UpdateUserValidator"/>.</description></item>
    ///     <item><description>Checks if a user with the provided email already exists.</description></item>
    ///     <item><description>Maps the command to a <see cref="User"/> entity.</description></item>
    ///     <item><description>Hashes the user's password for secure storage.</description></item>
    ///     <item><description>Updates the user in the repository.</description></item>
    ///     <item><description>Maps the updated user to an <see cref="UpdateUserResult"/> and returns it.</description></item>
    /// </list>
    /// </remarks>
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserHandler"/>.
        /// </summary>
        /// <param name="userRepository">The user repository used to access and update user data.</param>
        /// <param name="mapper">The AutoMapper instance used to map between objects.</param>
        /// <param name="passwordHasher">The password hasher used to securely hash user passwords.</param>
        public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        /// Handles the <see cref="UpdateUserCommand"/> request.
        /// </summary>
        /// <param name="command">The command containing the data to update the user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// An <see cref="UpdateUserResult"/> containing the updated user details.
        /// </returns>
        /// <exception cref="ValidationException">
        /// Thrown if the <see cref="UpdateUserCommand"/> fails validation.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if a user with the provided email already exists.
        /// </exception>
        public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserValidator();

            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

            if (existingUser != null && existingUser.Id != command.Id)
            {
                throw new InvalidOperationException($"User with email {command.Email} already exists");
            }

            var user = await _userRepository.GetByIdAsync(command.Id, cancellationToken) ?? throw new InvalidOperationException($"User not found.");

            user.Email = command.Email;
            user.Username = command.Username;
            user.Password = _passwordHasher.HashPassword(command.Password);
            user.Firstname = command.Name.Firstname;
            user.Lastname = command.Name.Lastname;
            user.City = command.Address.City;
            user.Street = command.Address.Street;
            user.Number = command.Address.Number;
            user.Zipcode = command.Address.Zipcode;
            user.GeolocationLat = command.Address.Geolocation.Lat;
            user.GeolocationLong = command.Address.Geolocation.Long;
            user.Phone = command.Phone;
            user.Status = command.Status;
            user.Role = command.Role;
            user.UpdatedAt = DateTime.UtcNow;

            var updatedUser = await _userRepository.UpdateAsync(user, cancellationToken);

            var result = _mapper.Map<UpdateUserResult>(updatedUser);

            return result;
        }
    }

}
