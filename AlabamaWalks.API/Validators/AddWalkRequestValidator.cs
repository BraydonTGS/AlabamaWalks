using AlabamaWalks.API.Models.DTO;
using FluentValidation;

namespace AlabamaWalks.API.Validators
{
    public class AddWalkRequestValidator : AbstractValidator<AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0); 
        }
    }
}
