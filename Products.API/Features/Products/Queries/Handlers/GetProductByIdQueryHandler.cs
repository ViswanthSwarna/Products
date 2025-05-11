using MediatR;
using Products.API.Features.Products.Queries;
using Products.Domain.Entities;
using Products.Repository.Interfaces;
using Serilog;

namespace Products.Application.Features.Products.Queries.Handlers
{

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Handling GetProductByIdQuery for ID {Id}", request.Id);
            return await _repository.GetByIdAsync(request.Id.ToString());
        } 
    }
}