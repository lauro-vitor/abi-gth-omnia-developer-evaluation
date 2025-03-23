using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales
{
    public class DeleteSalesCommand : IRequest<DeleteSalesResult>
    {
        public int Id { get; set; }
    }
}
