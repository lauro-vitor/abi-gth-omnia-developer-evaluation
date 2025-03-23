using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetByIdSales
{
    public class GetByIdSalesCommand : IRequest<SaleResult>
    {
        public int Id { get; set; }
    }
}
