using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ActivateProductItem
{
    public class ActivateProductItemHandler : IRequestHandler<ActivateProductItemCommand, SaleResult>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public ActivateProductItemHandler(IMapper mapper, ISaleRepository saleRepository)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        public async Task<SaleResult> Handle(ActivateProductItemCommand command, CancellationToken cancellationToken)
        {

            var sale = await _saleRepository.GetBydProductItemAsync(command.ProductItemId, cancellationToken);

            if (sale == null)
            {
                throw new InvalidOperationException("Sale not found.");
            }

            if (sale.IsCancelled())
            {
                throw new InvalidOperationException("To activate an item from sale, the sale must not be cancelled.");
            }

            var productItem = sale.SaleProductItems.FirstOrDefault(i => i.Id == command.ProductItemId);

            if (productItem == null)
            {
                throw new InvalidOperationException("Product Item not found.");
            }

            productItem.Activate();

            sale.CalculateTotals();

            sale.UpdatedAt = DateTime.UtcNow;

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            var result = _mapper.Map<SaleResult>(sale);

            return result;
        }
    }
}
