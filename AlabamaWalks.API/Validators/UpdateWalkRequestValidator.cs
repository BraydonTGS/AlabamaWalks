using AlabamaWalks.API.Models.DTO;
using FluentValidation;

namespace AlabamaWalks.API.Validators
{
    public class UpdateWalkRequestValidator : AbstractValidator<UpdateWalkDifficultyRequest>
    {
        public UpdateWalkRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty(); 
        }
    }
}
