using MediatR;
using Products.Domain.Entities;

namespace Products.API.Features.Products.Commands
{
    public record CreateProductCommand(Product Product) : IRequest<Product>;
    public record UpdateProductCommand(int Id, Product Product) : IRequest;
    public record DeleteProductCommand(int Id) : IRequest;
    public record AddToStockCommand(int Id, int Quantity) : IRequest;
    public record DecrementStockCommand(int Id, int Quantity) : IRequest;
}