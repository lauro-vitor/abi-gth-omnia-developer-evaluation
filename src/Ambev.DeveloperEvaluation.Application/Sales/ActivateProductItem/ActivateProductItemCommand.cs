using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ActivateProductItem
{
    public class ActivateProductItemCommand :  IRequest<SaleResult>
    {
        public int ProductItemId { get; set; }
    }
}
