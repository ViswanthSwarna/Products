using MediatR;
using Products.API.Exceptions.Products.Application.Exceptions;
using Products.API.Exceptions;
using Products.Domain.Entities;
using Products.Repository.Interfaces;
using Serilog;

namespace Products.API.Features.Products.Commands.Handlers
{

    public class DecrementStockCommandHandler : IRequestHandler<DecrementStockCommand>
    {
        private readonly IProductRepository _repository;

        public DecrementStockCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DecrementStockCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Handling AddToStockCommand for ID {Id}", request.Id);
            var product = await _repository.GetByIdAsync(request.Id.ToString());

            if (product == null) 
            {
                Log.Warning("Product with ID {Id} not found", request.Id);
                throw new NotFoundException(nameof(Product), request.Id);
            }


            if (product.StockAvailable < request.Quantity) 
            {
                Log.Warning("StockAvailable is less than requested decrement for ID {Id}", request.Id);
                throw new InsufficientStockException(request.Id, request.Quantity, product.StockAvailable);
            }           

            product.StockAvailable -= request.Quantity;
            await _repository.UpdateAsync(product);
        }
    }
}