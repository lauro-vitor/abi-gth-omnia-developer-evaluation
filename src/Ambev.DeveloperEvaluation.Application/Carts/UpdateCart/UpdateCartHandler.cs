using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartHandler : IRequestHandler<UpdateCartCommand, UpdateCartResult>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;

        public UpdateCartHandler(
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

        public async Task<UpdateCartResult> Handle(UpdateCartCommand command, CancellationToken cancellationToken)
        {
            var productsCommand = command.Products;

            var cart = await _cartRepository.GetByIdAsync(command.CartId, cancellationToken);

            var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found.");
            }

            if (cart.Status == CartStatus.SaleConfirmed)
            {
                throw new InvalidOperationException("It is not possible to update the cart because it has a confirmed sale status.");
            }

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            cart.Update(command.Date, user);

            if (productsCommand != null && productsCommand.Count > 0)
            {
                var productsIds = productsCommand.Select(p => p.ProductId).ToArray();

                var products = await _productRepository.GetAllAsync(productsIds, cancellationToken);

                cart.RemoveProductItems(products);

                foreach (var productCommandItem in productsCommand)
                {
                    var product = products.Find(p => p.Id == productCommandItem.ProductId);

                    int quantity = productCommandItem.Quantity;

                    if (product == null)
                    {
                        throw new InvalidOperationException("Product not found.");
                    }

                    if (cart.ExistsProductInCart(product))
                    {
                        cart.UpdateProductItem(product, quantity);
                    }
                    else
                    {
                        cart.AddProductItem(product, quantity);
                    }
                }
            }
            else
            {
                cart.ClearProductItems();
            }

            await _cartRepository.UpdateAsync(cart, cancellationToken);

            var result = _mapper.Map<UpdateCartResult>(cart);

            return result;
        }
    }
}