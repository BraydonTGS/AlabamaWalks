using AlabamaWalks.API.Models.DTO;
using FluentValidation;

namespace AlabamaWalks.API.Validators
{
    public class AddWalkDifficultyRequestValidator : AbstractValidator<AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty(); 
        }
    }
}
