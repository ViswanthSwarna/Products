using FluentValidation.TestHelper;
using Products.API.Models;
using Products.API.Validators;

namespace Products.UnitTests.Validators
{
    [TestFixture]
    public class ProductValidatorTests
    {
        private ProductValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new ProductValidator();
        }

        [Test]
        public void Should_Pass_When_ValidProduct()
        {
            var model = new CreateOrUpdateProductDto
            {
                Name = "Laptop",
                Description = "A good laptop",
                Price = 1200,
                StockAvailable = 5
            };

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Should_Fail_When_Name_IsEmpty()
        {
            var model = new CreateOrUpdateProductDto { Name = "" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.Name)
                  .WithErrorMessage("Name is required.");
        }

        [Test]
        public void Should_Fail_When_Name_TooLong()
        {
            var model = new CreateOrUpdateProductDto
            {
                Name = new string('a', 101)
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.Name)
                  .WithErrorMessage("Name must not exceed 100 characters.");
        }

        [Test]
        public void Should_Fail_When_Description_TooLong()
        {
            var model = new CreateOrUpdateProductDto
            {
                Description = new string('b', 501)
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.Description)
                  .WithErrorMessage("Description must not exceed 500 characters.");
        }

        [Test]
        public void Should_Fail_When_Price_IsNegative()
        {
            var model = new CreateOrUpdateProductDto
            {
                Price = -1
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.Price)
                  .WithErrorMessage("Price must be zero or greater.");
        }

        [Test]
        public void Should_Fail_When_StockAvailable_IsNegative()
        {
            var model = new CreateOrUpdateProductDto
            {
                StockAvailable = -5
            };

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(p => p.StockAvailable)
                  .WithErrorMessage("Stock must be zero or greater.");
        }
    }
}