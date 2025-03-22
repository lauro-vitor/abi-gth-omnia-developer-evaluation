using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetByIdCart
{
    public class GetByIdCartHandler : IRequestHandler<GetByIdCartCommand, GetByIdCartResult?>
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;


        public GetByIdCartHandler(IMapper mapper, ICartRepository cartRepository)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
        }

        public async Task<GetByIdCartResult?> Handle(GetByIdCartCommand request, CancellationToken cancellationToken)
        {
           var cart = await _cartRepository.GetByIdAsNoTrackingAsync(request.Id, cancellationToken);

            if (cart == null) 
            {
                return null;
            }

            var result = _mapper.Map<GetByIdCartResult>(cart);

            return result;
        }
    }
}
