using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Profile for mapping GetUser feature requests to commands
/// </summary>
public class GetUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetUser feature
    /// </summary>
    public GetUserProfile()
    {
        CreateMap<Guid, Application.Users.GetUser.GetUserCommand>()
            .ConstructUsing(id => new Application.Users.GetUser.GetUserCommand(id));

        CreateMap<GetUserResult, GetUserResponse>()
            .ForPath(dest => dest.Name.Firstname, opt => opt.MapFrom(src => src.Name.Firstname))
            .ForPath(dest => dest.Name.Lastname, opt => opt.MapFrom(src => src.Name.Lastname))
            .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.Address.City))
            .ForPath(dest => dest.Address.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForPath(dest => dest.Address.Number, opt => opt.MapFrom(src => src.Address.Number))
            .ForPath(dest => dest.Address.Zipcode, opt => opt.MapFrom(src => src.Address.Zipcode))
            .ForPath(dest => dest.Address.Geolocation.Lat, opt => opt.MapFrom(src => src.Address.Geolocation.Lat))
            .ForPath(dest => dest.Address.Geolocation.Long, opt => opt.MapFrom(src => src.Address.Geolocation.Long));
    }
}
