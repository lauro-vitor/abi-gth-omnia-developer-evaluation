using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.SalesResult
{
    public class SaleResultProfile : Profile
    {
        public SaleResultProfile()
        {
            CreateMap<Sale, SaleResult>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SaleNumber, opt => opt.MapFrom(src => src.SaleNumber))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Discounts, opt => opt.MapFrom(src => src.Discounts))
                .ForMember(dest => dest.TotalItemsAmount, opt => opt.MapFrom(src => src.TotalItemsAmount))
                .ForMember(dest => dest.TotalSaleAmount, opt => opt.MapFrom(src => src.TotalSaleAmount))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => MapUserFromSale(src)))
                .ForMember(dest => dest.ProductItems, opt => opt.MapFrom(src => src.SaleProductItems.Select(i => MapProductItemFromSaleProductItem(i))));
        }

        private static SaleUserResult MapUserFromSale(Sale sale)
        {
            var user = sale.User;

            var saleUserResult = new SaleUserResult()
            {
                Id = user.Id,
                Email = user.Email,
                Phone = user.Phone,
                Name = new SaleUserNameResult
                {
                     Firstname = user.Firstname,
                     Lastname = user.Lastname,
                },
                Address = new SaleUserAddressResult()
                {
                    City = user.City,
                    Number = user.Number,
                    Street = user.Street,
                    Zipcode = user.Zipcode  
                }
            };

            return saleUserResult;
        }
        private static SaleProductItemResult MapProductItemFromSaleProductItem(SaleProductItem saleProductItem)
        {
            var saleProductItemResult = new SaleProductItemResult()
            {
                Id = saleProductItem.Id,
                Quantity = saleProductItem.Quantity,
                Status =saleProductItem.Status.ToString(),
                TotalAmount = saleProductItem.TotalAmount,
                UnitPrice = saleProductItem.UnitPrice,
                ProductName = saleProductItem.Product.Title
            };

            return saleProductItemResult;
        }
    }
}
