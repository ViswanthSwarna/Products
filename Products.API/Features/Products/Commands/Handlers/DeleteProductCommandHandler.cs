using MediatR;
using Products.Repository.Interfaces;
using Serilog;

namespace Products.API.Features.Products.Commands.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Handling DeleteProductCommand for ID {Id}", request.Id);
            await _repository.DeleteAsync(request.Id.ToString());
        }
    }
}