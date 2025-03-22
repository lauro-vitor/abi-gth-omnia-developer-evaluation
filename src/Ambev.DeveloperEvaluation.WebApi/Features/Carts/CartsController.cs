using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetByIdCart;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{

    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartsController(IMediator mediator, IMapper mapper)
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UpdateCartResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCartRequest request)
        {
            var command = _mapper.Map<UpdateCartCommand>(request);

            command.CartId = id;

            var result = await _mediator.Send(command);

            var response = _mapper.Map<UpdateCartResponse>(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetByIdCartResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var command = new GetByIdCartCommand
            {
                Id = id
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteCartResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeleteCartCommand
            {
                Id = id
            };

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
