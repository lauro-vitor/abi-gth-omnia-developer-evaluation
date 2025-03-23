using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSales
{
    public class CreateSalesHandler : IRequestHandler<CreateSalesCommand, SaleResult>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly ISaleRepository _saleRepository;

        public CreateSalesHandler(IMapper mapper, ICartRepository cartRepository, ISaleRepository saleRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _saleRepository = saleRepository;
        }

        public async Task<SaleResult> Handle(CreateSalesCommand command, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByIdAsync(command.CartId, cancellationToken);

            if (cart == null) 
            {
                throw new InvalidOperationException("Cart not found.");
            }

            var sale = new Sale
            {
                Branch = command.Branch,
                Date = command.Date,
                Cart = cart,
                CartId = cart.Id,
                User = cart.User,
                UserId = cart.User.Id,
                CreatedAt = DateTime.UtcNow,
                Status = SaleStatus.Active,
                SaleNumber = await _saleRepository.GetNextSaleNumberAsync(cancellationToken)
            };

            sale.Validate();
            sale.AddProductProductsItems(cart);
            sale.CalculateTotals();

            cart.SetStatusAsSaleConfirmed();

            await _saleRepository.CreateAsync(sale, cancellationToken);

            var result = _mapper.Map<SaleResult>(sale);

            return result;
        }
    }
}
