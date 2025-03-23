using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ActivateSales
{
    public class ActivateSalesCommand : IRequest<SaleResult>
    {
        public int Id { get; set; }
    }
}
