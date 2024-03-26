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
            When(beer => beer.PromoPrice != null, () => {
                RuleFor(beer => beer.PromoPrice).LessThan(beer => beer.Price).WithMessage("Promo price not to be greater than regular price");
            });

            // Solution was in the logic of GreaterThan that should have been LessThan

            // We are trying to make sure that the beer promo price is not 0, and is not less than half the price of the regular price
            // issue is that it is checking it, but it's not stopping it 
        }
    }
}
