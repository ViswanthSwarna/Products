using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.API.Models;
using FluentValidation;
using Products.API.Features.Products.Commands;
using Products.API.Features.Products.Queries;
using Products.Domain.Entities;
using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Products.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrUpdateProductDto> _createOrUpdateProductValidator;

        public ProductsController(IMediator mediator, IMapper mapper, IValidator<CreateOrUpdateProductDto> createProductValidator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _createOrUpdateProductValidator = createProductValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Log.Information("GET /api/products called");
            var products = await _mediator.Send(new GetAllProductsQuery());
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            Log.Information("Fetched {Count} products", products.Count());
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Log.Information("GET /api/products/{Id} called", id);
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            if (product == null) 
            {
                Log.Warning("Product with ID {Id} not found", id);
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateProductDto createProductDto)
        {
            var validationResult = await _createOrUpdateProductValidator.ValidateAsync(createProductDto);
            if (!validationResult.IsValid)
            {
                Log.Warning("Validation failed for product creation: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                return BadRequest(validationResult.Errors);
            }

            var product = _mapper.Map<Product>(createProductDto);
            var createdProduct = await _mediator.Send(new CreateProductCommand(product));
            Log.Information("Product created with ID: {Id}", createdProduct.Id);

            var productDto = _mapper.Map<ProductDto>(createdProduct);

            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateOrUpdateProductDto updateProductDto)
        {
            var validationResult = await _createOrUpdateProductValidator.ValidateAsync(updateProductDto);
            if (!validationResult.IsValid) 
            {
                Log.Warning("Validation failed for product creation: {Errors}", string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                return BadRequest(validationResult.Errors);
            }

            var product = _mapper.Map<Product>(updateProductDto);
            product.Id = id.ToString();

            await _mediator.Send(new UpdateProductCommand(id, product));
            Log.Information("Product with ID {Id} updated", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProductCommand(id));
            Log.Information("Product with ID {Id} deleted", id);
            return NoContent();
        }

        [HttpPut("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {
            await _mediator.Send(new AddToStockCommand(id, quantity));
            Log.Information("Added {Quantity} to stock for product ID {Id}", quantity, id);
            return Ok();
        }

        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            await _mediator.Send(new DecrementStockCommand(id, quantity));
            Log.Information("Decremented {Quantity} from stock for product ID {Id}", quantity, id);
            return Ok();
        }
    }
}
