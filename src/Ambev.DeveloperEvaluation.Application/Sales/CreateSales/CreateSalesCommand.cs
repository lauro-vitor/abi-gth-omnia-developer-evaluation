using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSalesCommand : IRequest<SaleResult>
    {
        public int CartId { get; set; }
        public string Branch { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
