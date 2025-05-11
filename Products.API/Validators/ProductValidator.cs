using FluentValidation;
using Products.API.Models;
using Products.Domain.Entities;

namespace Products.API.Validators
{
    public class ProductValidator : AbstractValidator<CreateOrUpdateProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be zero or greater.");

            RuleFor(p => p.StockAvailable)
                .NotNull().WithMessage("StockAvailable is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Stock must be zero or greater.");
        }
    }
}