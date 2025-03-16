using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.QueryResult.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.GetUser;

/// <summary>
/// Profile for mapping between User entity and GetUserResponse
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser operation
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<User, GetUserResult>()
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => new NameQueryResult
        {
            Firstname = src.Firstname,
            Lastname = src.Lastname
        }))
        .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new AddressQueryResult
        {
            City = src.City,
            Street = src.Street,
            Number = src.Number,
            Zipcode = src.Zipcode,
            Geolocation = new GeolocationQueryResult
            {
                Lat = src.GeolocationLat,
                Long = src.GeolocationLong
            }
        }))
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
        .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
    }
}
