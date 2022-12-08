using AlabamaWalks.API.Models.DTO;
using FluentValidation;

namespace AlabamaWalks.API.Validators
{
    // Using Fluent Validations: AbstractValidator Requires a Type //
    public class AddRegionRequestValidator : AbstractValidator<AddRegionRequest>
    {
        // Use the CTOR to Define the rules that we want to validate //
        public AddRegionRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);

        }
    }
}
