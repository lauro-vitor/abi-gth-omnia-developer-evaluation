using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartProfile : Profile
    {
        public CreateCartProfile()
        {
            CreateMap<Cart, CreateCartResult>()
                .ForPath(
                dest => dest.Products, 
                src => src.MapFrom(c => c.CartProductItems.Select(i => new CreateCartProductItem
                {
                    ProductId = i.ProductId,
                    Quantity  = i.Quantity
                }))
              );
        }
    }
}
