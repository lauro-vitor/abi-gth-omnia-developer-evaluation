using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetAllCategories;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory;
using Ambev.DeveloperEvaluation.Application.Products.GetByIdProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.QueryResult;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetByIdProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;


[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateProductCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<CreateProductResponse>(result);

        return Created(string.Empty, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new GetByIdProductCommand { Id = id };

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<GetByIdProductResponse>(result);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<GetAllProductCommand>(request);

        var result = await _mediator.Send(command, cancellationToken);

        var paginatedList = await PaginatedList<ProductQueryResult>.CreateAsync(
            result.QueryProducts, 
            request.PageNumber, 
            request.PageSize
          );

        return OkPaginated(paginatedList);
    }


    [HttpGet("categories/{category}")]
    public async Task<IActionResult> GetAllProductsByCategory([FromRoute] string category,[FromQuery] GetAllProductRequest request, CancellationToken cancellationToken)
    {
        var command = new GetAllProductsByCategoryCommand
        {
            Category = category,
            Order = request.Order,
        };

        var result = await _mediator.Send(command, cancellationToken);

        var paginatedList = await PaginatedList<ProductQueryResult>.CreateAsync(
            result.QueryProducts,
            request.PageNumber,
            request.PageSize
          );

        return OkPaginated(paginatedList);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var command = new GetAllCategoriesCommand();

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result.Categories);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateProductCommand>(request);

        command.Id = id;

        var result = await _mediator.Send(command, cancellationToken);

        var response = _mapper.Map<UpdateProductResponse>(result);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id, CancellationToken cancellationToken)
    {
        var command = new DeleteProductCommand { Id = id };

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}

