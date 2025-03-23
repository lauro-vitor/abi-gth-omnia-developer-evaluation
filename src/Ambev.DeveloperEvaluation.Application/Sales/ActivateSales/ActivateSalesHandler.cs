using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ActivateSales
{
    public class ActivateSalesHandler : IRequestHandler<ActivateSalesCommand, SaleResult>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public ActivateSalesHandler(IMapper mapper, ISaleRepository saleRepository)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
        }

        public async Task<SaleResult> Handle(ActivateSalesCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

            if (sale == null)
            {
                throw new InvalidOperationException("Sale not found.");
            }

            sale.Activate();

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            var result = _mapper.Map<SaleResult>(sale);

            return result;
        }
    }
}
