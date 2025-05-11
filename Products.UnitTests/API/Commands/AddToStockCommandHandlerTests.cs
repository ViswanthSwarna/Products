using Moq;
using Products.API.Exceptions;
using Products.API.Features.Products.Commands;
using Products.API.Features.Products.Commands.Handlers;
using Products.Domain.Entities;
using Products.Repository.Interfaces;

namespace Products.UnitTests.API.Commands
{
    [TestFixture]
    public class AddToStockCommandHandlerTests
    {
        private Mock<IProductRepository> _mockProductRepo;
        private AddToStockCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _handler = new AddToStockCommandHandler(_mockProductRepo.Object);
        }

        [Test]
        public async Task Handle_ProductExists_AddsStock()
        {
            // Arrange
            var product = new Product { Id = "123", StockAvailable = 10 };
            _mockProductRepo.Setup(r => r.GetByIdAsync("123"))
                     .ReturnsAsync(product);

            var command = new AddToStockCommand(123, 5);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(product.StockAvailable, Is.EqualTo(15));
            _mockProductRepo.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        [Test]
        public void Handle_ProductNotFound_ThrowsNotFoundException()
        {
            // Arrange
            _mockProductRepo.Setup(r => r.GetByIdAsync("999")).ReturnsAsync((Product)null);
            var command = new AddToStockCommand(999, 5);

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(command, CancellationToken.None));
            Assert.That(ex.Message, Does.Contain("Product"));
        }
    }
}