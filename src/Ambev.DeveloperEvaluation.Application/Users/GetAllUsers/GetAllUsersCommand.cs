using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

public class GetAllUsersCommand : IRequest<GetAllUsersResult>
{
    public string Order { get; set; } = string.Empty;
}

