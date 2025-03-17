using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.QueryResult;
using Ambev.DeveloperEvaluation.Domain.QueryResult.Users;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersCommand, GetAllUsersResult>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<GetAllUsersResult> Handle(GetAllUsersCommand command, CancellationToken cancellationToken)
    {
        var queryUsers = _userRepository.GetAllUsers();

        queryUsers = SortUsers(command.Order, queryUsers);

        var queryUsersResult = queryUsers.Select(user => new UserQueryResult
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            Password = user.Password,
            Name = new NameQueryResult
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname
            },
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
            },
            Phone = user.Phone,
            Status = user.Status.ToString(),
            Role = user.Role.ToString()
        });

        var result = new GetAllUsersResult
        {
            QueryUsersResult = queryUsersResult
        };

        return Task.FromResult(result);
    }


    private static IQueryable<User> SortUsers(string? order, IQueryable<User> query)
    {
        if (string.IsNullOrWhiteSpace(order))
        {
            return query.OrderBy(p => p.CreatedAt);
        }

        var sortClauses = order.Split(',')
            .Select(o => o.Trim())
            .Where(o => !string.IsNullOrEmpty(o))
            .ToArray();

        bool isFirst = true;

        foreach (var sortClauseItem in sortClauses)
        {
            var sortCriteria = sortClauseItem.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (sortCriteria.Length == 0)
            {
                continue;
            }

            var sortProperty = sortCriteria[0].Trim();

            var sortDirection = sortCriteria.ElementAtOrDefault(1)?.Trim().ToLower() ?? "asc";

            Expression<Func<User, object>> sortExpression = sortProperty switch
            {
                "username" => u => u.Username,
                "firstname" => u => u.Firstname,
                "lastname" => u => u.Lastname,
                "email" => u => u.Email,
                "phone" => u => u.Phone,
                "role" => u => u.Role,
                "status" => u => u.Status,
                "city" => u => u.City,
                "street" => u => u.Street,
                "number" => u => u.Number ?? 0,
                "zipcode" => u => u.Zipcode,
                "lat" => u => u.GeolocationLat,
                "long" => u => u.GeolocationLong,
                "id" => u => u.Id,
                _ => u => u.CreatedAt,
            };

            if (isFirst)
            {
                query = sortDirection == "desc" ?
                    query.OrderByDescending(sortExpression) :
                    query.OrderBy(sortExpression);

                isFirst = false;
            }
            else
            {
                query = sortDirection == "desc" ?
                    ((IOrderedQueryable<User>)query).ThenByDescending(sortExpression) :
                    ((IOrderedQueryable<User>)query).ThenBy(sortExpression);
            }
        }

        return query;
    }
}

