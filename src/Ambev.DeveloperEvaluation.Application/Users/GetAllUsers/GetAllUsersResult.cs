using Ambev.DeveloperEvaluation.Domain.QueryResult.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

public class GetAllUsersResult
{
    public IQueryable<UserQueryResult> QueryUsersResult { get; set; }

    public GetAllUsersResult()
    {
        QueryUsersResult = new List<UserQueryResult>().AsQueryable();
    }

}

