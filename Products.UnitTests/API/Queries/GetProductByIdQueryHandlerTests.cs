using Moq;
using Products.API.Features.Products.Queries;
using Products.Application.Features.Products.Queries.Handlers;
using Products.Domain.Entities;
using Products.Repository.Interfaces;

namespace Products.UnitTests.API.Queries
{
    [TestFixture]
    public class GetProductByIdQueryHandlerTests
    {
        private Mock<IProductRepository> _productRepoMock;
        private GetProductByIdQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _handler = new GetProductByIdQueryHandler(_productRepoMock.Object);
        }

        [Test]
        public async Task Handle_ReturnsProduct_WhenFound()
        {
            // Arrange
            var product = new Product { Id = "1", Name = "Test Product", StockAvailable = 15 };
            _productRepoMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(product);
            var query = new GetProductByIdQuery(1);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Product", result.Name);
            _productRepoMock.Verify(r => r.GetByIdAsync("1"), Times.Once);
        }

        [Test]
        public async Task Handle_ReturnsNull_WhenNotFound()
        {
            // Arrange
            _productRepoMock.Setup(r => r.GetByIdAsync("99")).ReturnsAsync((Product?)null);
            var query = new GetProductByIdQuery(99);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
            _productRepoMock.Verify(r => r.GetByIdAsync("99"), Times.Once);
        }
    }
}