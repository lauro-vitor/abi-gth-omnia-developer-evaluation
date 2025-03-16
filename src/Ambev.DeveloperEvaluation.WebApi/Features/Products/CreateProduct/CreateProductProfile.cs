using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        CreateMap<CreateProductRequest, CreateProductCommand>()
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rating.Rate)) 
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Rating.Count));

        CreateMap<CreateProductResult, CreateProductResponse>()
            .ForPath(dest => dest.Rating.Rate, opt => opt.MapFrom(src => src.Rate))
            .ForPath(dest => dest.Rating.Count,  opt => opt.MapFrom(src => src.Count));

         //.ForMember(dest => dest.Rating.Rate, opt => opt.MapFrom(src => src.Rate))
         //   .ForMember(dest => dest.Rating.Count, opt => opt.MapFrom(src => src.Count));
    }
}

