using Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales
{
    public class UpdateSalesProfile :  Profile
    {
        public UpdateSalesProfile()
        {
            CreateMap<UpdateSalesRequest, UpdateSalesCommand>();
        }
    }
}
