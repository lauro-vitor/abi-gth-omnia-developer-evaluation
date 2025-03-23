using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelProductItem
{
    public class CancelProductItemHandler : IRequestHandler<CancelProductItemCommand, SaleResult>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public CancelProductItemHandler(IMapper mapper, ISaleRepository saleRepository)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        public async Task<SaleResult> Handle(CancelProductItemCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetBydProductItemAsync(command.Id, cancellationToken);

            if (sale == null)
            {
                throw new InvalidOperationException("Sale not found.");
            }

            if (sale.IsCancelled())
            {
                throw new InvalidOperationException("To cancel an item from sale, the sale must not be cancelled.");
            }

            var productItem = sale.SaleProductItems.FirstOrDefault(i => i.Id == command.Id);

            if (productItem == null)
            {
                throw new InvalidOperationException("Product Item not found.");
            }

            productItem.Cancel();

            sale.CalculateTotals();

            sale.UpdatedAt = DateTime.UtcNow;

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            var result = _mapper.Map<SaleResult>(sale);

            return result;
        }
    }
}
