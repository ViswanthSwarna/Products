using MediatR;
using Products.Constants.Enums;
using Products.Domain.Entities;
using Products.Repository.Interfaces;
using Serilog;

namespace Products.API.Features.Products.Commands.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IProductRepository _repository;
        private readonly ICodeTrackerRepository _codeTrackerRepository;

        public CreateProductCommandHandler(IProductRepository repository, ICodeTrackerRepository codeTrackerRepository)
        {
            _repository = repository;
            _codeTrackerRepository = codeTrackerRepository;
        }

        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Handling CreateProductCommand");
            int id = await _codeTrackerRepository.GetNextCodeAsync(CodeTrackerKey.Products);
            request.Product.Id = id.ToString();
            await _repository.AddAsync(request.Product);
            return request.Product;
        }
    }
}