using Ambev.DeveloperEvaluation.Application.Products.GetByIdProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByIdProduct;

public class GetByIdProductProfile : Profile
{
    public GetByIdProductProfile()
    {
        CreateMap<GetByIdProductResult, GetByIdProductResponse>()
            .ForPath(dest => dest.Rating.Rate, opt => opt.MapFrom(src => src.Rate))
            .ForPath(dest => dest.Rating.Count, opt => opt.MapFrom(src => src.Count));
    }
}

