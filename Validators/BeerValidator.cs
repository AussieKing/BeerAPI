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
                RuleFor(beer => beer.PromoPrice).LessThan(beer => beer.Price).WithMessage("Promo price must be less than regular price");
            });
        }
    }
}
