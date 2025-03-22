using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetByIdCart
{
    public class GetByIdCartProfile : Profile
    {
        public GetByIdCartProfile()
        {
            CreateMap<Cart, GetByIdCartResult>().ForPath(
                dest => dest.Products,
                src => src.MapFrom(c => c.CartProductItems.Select(i => new GetByIdCartProductItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }))
              );
        }
    }
}
