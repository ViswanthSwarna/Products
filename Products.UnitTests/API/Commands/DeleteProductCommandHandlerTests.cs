using Moq;
using Products.API.Features.Products.Commands.Handlers;
using Products.API.Features.Products.Commands;
using Products.Repository.Interfaces;

namespace Products.UnitTests.API.Commands
{
    [TestFixture]
    public class DeleteProductCommandHandlerTests
    {
        private Mock<IProductRepository> _productRepoMock;
        private DeleteProductCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _handler = new DeleteProductCommandHandler(_productRepoMock.Object);
        }

        [Test]
        public async Task Handle_ValidId_CallsDeleteAsync()
        {
            // Arrange
            var command = new DeleteProductCommand(1);
            _productRepoMock.Setup(r => r.DeleteAsync("1")).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepoMock.Verify(r => r.DeleteAsync("1"), Times.Once);
        }
    }
}