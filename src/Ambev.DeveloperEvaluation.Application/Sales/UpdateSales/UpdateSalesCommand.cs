using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSales
{
    public class UpdateSalesCommand : IRequest<SaleResult>
    {
        public int Id { get; set; }
        public string Branch { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
