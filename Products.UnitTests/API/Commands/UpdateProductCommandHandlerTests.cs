using Moq;
using Products.API.Features.Products.Commands.Handlers;
using Products.API.Features.Products.Commands;
using Products.Domain.Entities;
using Products.Repository.Interfaces;

namespace Products.UnitTests.API.Commands
{
    [TestFixture]
    public class UpdateProductCommandHandlerTests
    {
        private Mock<IProductRepository> _productRepoMock;
        private UpdateProductCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _handler = new UpdateProductCommandHandler(_productRepoMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_CallsUpdateAsyncWithCorrectProduct()
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                StockAvailable = 50
            };

            var command = new UpdateProductCommand(123, product);

            _productRepoMock
                .Setup(r => r.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepoMock.Verify(r =>
                r.UpdateAsync(It.Is<Product>(p =>
                    p.Id == "123" &&
                    p.Name == "Test Product" &&
                    p.StockAvailable == 50)),
                Times.Once);
        }
    }
}