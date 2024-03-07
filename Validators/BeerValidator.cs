// Working with FluentValidation
// Step 1: Create a new file called BeerValidator.cs in the Validators folder
// Step 2: Add the validation rules for the Beer model using FluentValidation

using FluentValidation;
using BeerAPI.Models;

namespace BeerAPI.Validators 
{
    public class BeerValidator : AbstractValidator<Beer> // Declaring the BeerValidator class, which inherits from AbstractValidator<T>, where T is the type being validated (in this case, Beer)
    {
        public BeerValidator() // Constructor function
        {
            // Defining all rules for the Beer model below
            RuleFor(beer => beer.Name).NotEmpty().WithMessage("Beer name is required.");
            RuleFor(beer => beer.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(beer => beer.PromoPrice)
            .GreaterThan(0).When(beer => beer.PromoPrice.HasValue)
            .LessThan(beer => beer.Price).When(beer => beer.PromoPrice.HasValue)
            .WithMessage("Promo price must be greater than zero and less than the usual price.");
        }
    }
}
