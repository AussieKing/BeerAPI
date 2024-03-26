using FluentValidation;
using BeerAPI.Models;

namespace BeerAPI.Validators 
{
    public class BeerValidator : AbstractValidator<Beer> 
    {
        public BeerValidator() 
        {
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
