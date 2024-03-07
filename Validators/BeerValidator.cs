// Working with FluentValidation
// Step 1: Create a new file called BeerValidator.cs in the Validators folder
// Step 2: Add the validation rules for the Beer model using FluentValidation

using FluentValidation;
using BeerAPI.Models;

namespace BeerAPI.Validators // This matches the folder structure and the using directive in Program.cs
{
    public class BeerValidator : AbstractValidator<Beer>
    {
        public BeerValidator()
        {
            RuleFor(beer => beer.Name).NotEmpty().WithMessage("Beer name is required.");
            RuleFor(beer => beer.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(beer => beer.PromoPrice)
            .GreaterThan(0).When(beer => beer.PromoPrice.HasValue)
            .LessThan(beer => beer.Price).When(beer => beer.PromoPrice.HasValue)
            .WithMessage("Promo price must be greater than zero and less than the usual price.");
        }
    }
}
