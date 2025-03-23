using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSales
{
    public class DeleteSalesHandler : IRequestHandler<DeleteSalesCommand, DeleteSalesResult>
    {

        private readonly ISaleRepository _saleRepository;

        public DeleteSalesHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<DeleteSalesResult> Handle(DeleteSalesCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

            if (sale == null)
            {
                throw new InvalidOperationException("Sale not found.");
            }

            var resultDelete = await _saleRepository.DeleteAsync(command.Id, cancellationToken);

            var result = new DeleteSalesResult();

            if (resultDelete)
            {
                result.Message = "The sale has been successfully deleted.";
            }
            else
            {
                result.Message = "The sale deletion failed.";
            }

            return result;
        }
    }

}
