﻿using Ambev.DeveloperEvaluation.Application.Sales.ActivateProductItem;
using Ambev.DeveloperEvaluation.Application.Sales.ActivateSales;
using Ambev.DeveloperEvaluation.Application.Sales.CancelProductItem;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSales;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSales;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetByIdSales;
using Ambev.DeveloperEvaluation.Application.Sales.SalesResult;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSales;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SaleResult), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateSalesCommand command)
        {
            var result = await _mediator.Send(command);

            return Created(string.Empty, result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<SaleResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetAllSaleCommand command, 
            [FromQuery(Name = "_page")] int pageNumber = 1,
            [FromQuery(Name = "_size")] int pageSize = 10)
        {
            var result = await _mediator.Send(command);

            var paginaedList = await PaginatedList<SaleResult>.CreateAsync(result, pageNumber, pageSize);

            return OkPaginated(paginaedList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SaleResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var command = new GetByIdSalesCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SaleResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSalesRequest request)
        {
            var command = _mapper.Map<UpdateSalesCommand>(request);

            command.Id = id;

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("cancel/{id}")]
        [ProducesResponseType(typeof(SaleResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Cancel([FromRoute] int id)
        {
            var command = new CancelSalesCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("activate/{id}")]
        [ProducesResponseType(typeof(SaleResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Activate([FromRoute] int id)
        {
            var command = new ActivateSalesCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(DeleteSalesResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeleteSalesCommand { Id = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("product-item/cancel/{id}")]
        [ProducesResponseType(typeof(SaleResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelProductItem([FromRoute] int id)
        {
            var command = new CancelProductItemCommand { ProductItemId = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("product-item/activate/{id}")]
        [ProducesResponseType(typeof(SaleResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> ActivateProductItem([FromRoute] int id)
        {
            var command = new ActivateProductItemCommand { ProductItemId = id };

            var result = await _mediator.Send(command);

            return Ok(result);
        }

    }
}
