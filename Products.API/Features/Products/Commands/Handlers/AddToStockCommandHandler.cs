using MediatR;
using Products.API.Exceptions;
using Products.Domain.Entities;
using Products.Repository.Interfaces;
using Serilog;

namespace Products.API.Features.Products.Commands.Handlers
{
    public class AddToStockCommandHandler : IRequestHandler<AddToStockCommand>
    {
        private readonly IProductRepository _repository;

        public AddToStockCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(AddToStockCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Handling AddToStockCommand for ID {Id}", request.Id);

            var product = await _repository.GetByIdAsync(request.Id.ToString());
            if (product == null) 
            {
                Log.Warning("Product with ID {Id} not found", request.Id);
                throw new NotFoundException(nameof(Product), request.Id); 
            }
            product.StockAvailable += request.Quantity;
            await _repository.UpdateAsync(product);
        }
    }
}