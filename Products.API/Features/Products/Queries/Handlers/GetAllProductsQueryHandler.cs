using MediatR;
using Products.API.Features.Products.Commands;
using Products.API.Features.Products.Queries;
using Products.Domain.Entities;
using Products.Repository.Interfaces;
using Serilog;

namespace Products.Application.Features.Products.Queries.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            Log.Information("Handling GetAllProductsQuery");
            return await _repository.GetAllAsync();
        }
    }
}