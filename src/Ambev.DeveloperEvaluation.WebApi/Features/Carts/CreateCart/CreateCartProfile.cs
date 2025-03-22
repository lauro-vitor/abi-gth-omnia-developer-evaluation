using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart
{
    public class CreateCartProfile : Profile
    {
        public CreateCartProfile()
        {
            CreateMap<CreateCartRequest, CreateCartCommand>()
                .ForPath(
                    dest => dest.Products,
                    src => src.MapFrom(c => c.Products.Select(i => new Application.Carts.CreateCart.CreateCartProductItem()
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity
                    }))
                );


            CreateMap<CreateCartResult, CreateCartResponse>()
                 .ForPath(
                    dest => dest.Products,
                    src => src.MapFrom(c => c.Products.Select(i => new CreateCartProductItem()
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity
                    }))
                );
        }
    }
}
