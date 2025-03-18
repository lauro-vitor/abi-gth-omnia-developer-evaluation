using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers;
using Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;
using Ambev.DeveloperEvaluation.Domain.QueryResult.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users;

/// <summary>
/// Controller for managing user operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of UsersController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public UsersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="request">The user creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateUserCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<CreateUserResponse>(result);

        return Created(string.Empty, response);
    }

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    /// <remarks>
    /// This endpoint updates an existing user in the system based on the provided ID and request data.
    /// The request data is validated using <see cref="UpdateUserRequestValidator"/> before processing.
    /// If the validation fails, a <see cref="BadRequestObjectResult"/> is returned with the validation errors.
    /// If the update is successful, the updated user details are returned.
    /// </remarks>
    /// <param name="id">The unique identifier of the user to be updated.</param>
    /// <param name="request">The request containing the updated user data.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// <list type="bullet">
    ///     <item><description><b>200 OK</b>: Returns the updated user details in the response body.</description></item>
    ///     <item><description><b>400 Bad Request</b>: Returns validation errors if the request data is invalid.</description></item>
    /// </list>
    /// </returns>
    [HttpPut("{id}")] // Alterado para HttpPut, que é mais adequado para operações de atualização
    [ProducesResponseType(typeof(UpdateUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(
        [FromRoute] Guid id,
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateUserCommand>(request);
        command.Id = id;

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<UpdateUserResponse>(result);

        return Ok(response);
    }

    /// <summary>dsadsa
    /// Retrieves a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new GetUserRequest { Id = id };
        var validator = new GetUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetUserCommand>(request.Id);
        var result = await _mediator.Send(command, cancellationToken);
        var response = _mapper.Map<GetUserResponse>(result);

        return Ok(response);
    }
    /// <summary>
    /// Retrieves all users
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of usres</returns>
    [HttpGet()]
    [ProducesResponseType(typeof(PaginatedList<UserQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<GetAllUsersCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var paginatedList = await PaginatedList<UserQueryResult>.CreateAsync(
            result.QueryUsersResult,
            request.PageNumber,
            request.PageSize
          );

        return OkPaginated(paginatedList);
    }

    /// <summary>
    /// Deletes a user by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the user was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteUserRequest { Id = id };
        var validator = new DeleteUserRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteUserCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "User deleted successfully"
        });
    }
}
