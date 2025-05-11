using MediatR;
using Products.Domain.Entities;

namespace Products.API.Features.Products.Queries
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<Product>>;
    public record GetProductByIdQuery(int Id) : IRequest<Product?>;
}