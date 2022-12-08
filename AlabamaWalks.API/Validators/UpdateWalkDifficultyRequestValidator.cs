using AlabamaWalks.API.Models.DTO;
using FluentValidation;

namespace AlabamaWalks.API.Validators
{
    public class UpdateWalkDifficultyRequestValidator : AbstractValidator<UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty(); 
        }
    }
}
