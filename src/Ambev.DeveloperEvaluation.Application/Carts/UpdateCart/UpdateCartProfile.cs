using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartProfile : Profile
    {
        public UpdateCartProfile()
        {
            CreateMap<Cart, UpdateCartResult>()
                .ForPath(
                dest => dest.Products,
                src => src.MapFrom(c => c.CartProductItems.Select(i => new UpdateCartProductItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }))
              );
        }
    }
}
