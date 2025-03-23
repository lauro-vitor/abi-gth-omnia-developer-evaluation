using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSales
{
    public class CancelSalesCommand : IRequest<SaleResult>
    {
        public int Id { get; set; }
    }
}
