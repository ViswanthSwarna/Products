using Moq;
using Products.API.Features.Products.Commands.Handlers;
using Products.API.Features.Products.Commands;
using Products.Domain.Entities;
using Products.Repository.Interfaces;
using Products.Constants.Enums;

namespace Products.UnitTests.API.Commands
{
    [TestFixture]
    public class CreateProductCommandHandlerTests
    {
        private Mock<IProductRepository> _productRepoMock;
        private Mock<ICodeTrackerRepository> _codeTrackerRepoMock;
        private CreateProductCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _productRepoMock = new Mock<IProductRepository>();
            _codeTrackerRepoMock = new Mock<ICodeTrackerRepository>();
            _handler = new CreateProductCommandHandler(_productRepoMock.Object, _codeTrackerRepoMock.Object);
        }

        [Test]
        public async Task Handle_AssignsGeneratedIdAndSavesProduct()
        {
            // Arrange
            var inputProduct = new Product { Name = "Test", StockAvailable = 5 };
            var command = new CreateProductCommand(inputProduct);

            _codeTrackerRepoMock.Setup(repo => repo.GetNextCodeAsync(CodeTrackerKey.Products))
                                .ReturnsAsync(100001);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Id, Is.EqualTo("100001"));
            _productRepoMock.Verify(r => r.AddAsync(It.Is<Product>(p => p.Id == "100001")), Times.Once);
        }
    }
}