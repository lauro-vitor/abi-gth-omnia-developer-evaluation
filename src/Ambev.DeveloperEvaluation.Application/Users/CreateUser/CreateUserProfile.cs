using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.QueryResult.Users;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Profile for mapping between User entity and CreateUserResponse
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser operation
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Name.Firstname))
            .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Name.Lastname))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Address.Zipcode))
            .ForMember(dest => dest.GeolocationLat, opt => opt.MapFrom(src => src.Address.Geolocation.Lat))
            .ForMember(dest => dest.GeolocationLong, opt => opt.MapFrom(src => src.Address.Geolocation.Long));


        CreateMap<User, CreateUserResult>()
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
