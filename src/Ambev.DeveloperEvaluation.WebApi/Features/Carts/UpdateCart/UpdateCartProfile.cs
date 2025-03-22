using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<UpdateCartRequest, UpdateCartCommand>()
                .ForPath(
                    dest => dest.Products,
                    src => src.MapFrom(c => c.Products.Select(i => new Application.Carts.UpdateCart.UpdateCartProductItem()
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity
                    }))
                );

            CreateMap<UpdateCartResult, UpdateCartResponse>()
                .ForPath(
                    dest => dest.Products,
                    src => src.MapFrom(c => c.Products.Select(i => new UpdateCartProductItem()
                    {
                        ProductId = i.ProductId,
                        Quantity = i.Quantity
                    }))
                );
        }
    }
}