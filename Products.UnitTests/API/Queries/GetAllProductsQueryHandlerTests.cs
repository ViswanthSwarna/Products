using Moq;
using Products.API.Features.Products.Queries;
using Products.Application.Features.Products.Queries.Handlers;
using Products.Domain.Entities;
using Products.Repository.Interfaces;

namespace Products.UnitTests.API.Queries
{
    [TestFixture]
    public class GetAllProductsQueryHandlerTests
    {
        private Mock<IProductRepository> _productRepoMock;
        private GetAllProductsQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _handler = new GetAllProductsQueryHandler(_productRepoMock.Object);
        }

        [Test]
        public async Task Handle_ReturnsAllProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = "1", Name = "Product A", StockAvailable = 10 },
                new Product { Id = "2", Name = "Product B", StockAvailable = 20 }
            };

            _productRepoMock
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(expectedProducts);

            var query = new GetAllProductsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Name, Is.EqualTo("Product A"));
            _productRepoMock.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}