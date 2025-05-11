using MediatR;
using Products.Repository.Interfaces;
using Serilog;

namespace Products.API.Features.Products.Commands.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository) 
        {
            _repository = repository;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Handling UpdateProductCommand for ID {Id}", request.Id);

            request.Product.Id = request.Id.ToString();
            await _repository.UpdateAsync(request.Product);
        }
    }
}