using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetByIdSales
{
    public class GetByIdSalesHandler : IRequestHandler<GetByIdSalesCommand, SaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetByIdSalesHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleResult> Handle(GetByIdSalesCommand command, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

            if(sale == null)
            {
                throw new InvalidOperationException("Sale not found.");
            }

            var result = _mapper.Map<SaleResult>(sale);

            return result;
        }
    }
}
