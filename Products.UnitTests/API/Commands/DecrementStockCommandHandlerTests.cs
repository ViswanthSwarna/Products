using Moq;
using Products.API.Exceptions.Products.Application.Exceptions;
using Products.API.Exceptions;
using Products.API.Features.Products.Commands.Handlers;
using Products.API.Features.Products.Commands;
using Products.Domain.Entities;
using Products.Repository.Interfaces;

namespace Products.UnitTests.API.Commands
{
    [TestFixture]
    public class DecrementStockCommandHandlerTests
    {
        private Mock<IProductRepository> _productRepoMock;
        private DecrementStockCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _handler = new DecrementStockCommandHandler(_productRepoMock.Object);
        }

        [Test]
        public void Handle_ProductNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var command = new DecrementStockCommand(1, 5);
            _productRepoMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync((Product)null);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_InsufficientStock_ThrowsInsufficientStockException()
        {
            // Arrange
            var command = new DecrementStockCommand(1, 10);
            var product = new Product { Id = "1", StockAvailable = 5 };

            _productRepoMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(product);

            // Act & Assert
            Assert.ThrowsAsync<InsufficientStockException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task Handle_ValidRequest_DecrementsStockAndUpdatesProduct()
        {
            // Arrange
            var command = new DecrementStockCommand(1, 5);
            var product = new Product { Id = "1", StockAvailable = 10 };

            _productRepoMock.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(product);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(product.StockAvailable, Is.EqualTo(5));
            _productRepoMock.Verify(r => r.UpdateAsync(product), Times.Once);
        }
    }
}