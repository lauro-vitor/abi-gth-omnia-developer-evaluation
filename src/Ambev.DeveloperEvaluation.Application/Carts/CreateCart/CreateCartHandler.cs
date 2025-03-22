using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
    {

        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;


        public CreateCartHandler(
            IMapper mapper,
            ICartRepository cartRepository,
            IProductRepository productRepository,
            IUserRepository userRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
        }

        public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var cart = new Cart();

            cart.Create(command.Date, user);

            var productsCommand = command.Products;

            if (productsCommand != null && productsCommand.Count > 0)
            {
                var productsIds = productsCommand.Select(p => p.ProductId).ToArray();

                var products = await _productRepository.GetAllAsync(productsIds, cancellationToken);

                foreach (var productCommandItem in productsCommand)
                {
                    var product = products.Find(p => p.Id == productCommandItem.ProductId);

                    if (product == null)
                    {
                        throw new InvalidOperationException("Product not found.");
                    }

                    cart.AddProductItem(cart.Id, product, productCommandItem.Quantity);
                }
            }

            await _cartRepository.CreateAsync(cart, cancellationToken);

            var result = _mapper.Map<CreateCartResult>(cart);

            return result;
        }
    }
}
