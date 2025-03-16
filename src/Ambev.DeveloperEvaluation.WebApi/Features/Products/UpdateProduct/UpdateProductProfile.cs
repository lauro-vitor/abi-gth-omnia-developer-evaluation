using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductProfile:Profile
{
    public UpdateProductProfile()
    {
        CreateMap<UpdateProductRequest, UpdateProductCommand>()
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rating.Rate))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Rating.Count));

        CreateMap<UpdateProductResult, UpdateProductResponse>()
        .ForPath(dest => dest.Rating.Rate, opt => opt.MapFrom(src => src.Rate))
        .ForPath(dest => dest.Rating.Count, opt => opt.MapFrom(src => src.Count));
    }
}

