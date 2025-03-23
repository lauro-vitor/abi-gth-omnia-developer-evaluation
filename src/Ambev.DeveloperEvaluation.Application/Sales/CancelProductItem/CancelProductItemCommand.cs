using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelProductItem
{
    public class CancelProductItemCommand : IRequest<SaleResult>
    {
        public int Id { get; set; }
    }
}
