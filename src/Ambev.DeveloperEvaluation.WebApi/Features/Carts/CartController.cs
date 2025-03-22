using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{

    [ApiController]
    [Route("api/[controller]")]
    public class CartController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateCartResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateCartRequest request)
        {
            var command = _mapper.Map<CreateCartCommand>(request);
            var result = await _mediator.Send(command);
            var response = _mapper.Map<CreateCartResponse>(result);

            return Created(string.Empty, response);
        }
    }
}
