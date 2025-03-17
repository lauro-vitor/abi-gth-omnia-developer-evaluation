using Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers;
public class GetAllUsersProfile : Profile
{
    public GetAllUsersProfile()
    {
        CreateMap<GetAllUsersRequest, GetAllUsersCommand>();
    }
}

