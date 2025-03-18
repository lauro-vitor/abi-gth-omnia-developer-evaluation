using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.QueryResult.Users;
using AutoMapper;


namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser
{
    /// <summary>
    /// Profile for mapping between User entity and UpdateUsers Command and results
    /// </summary>
    public class UpdateUserProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for UpdateUser operation
        /// </summary>
        public UpdateUserProfile()
        {

            CreateMap<User, UpdateUserResult>()
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

}
