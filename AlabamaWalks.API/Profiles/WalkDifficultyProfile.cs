using AlabamaWalks.API.Models.Domain;
using AlabamaWalks.API.Models.DTO;
using AutoMapper;

namespace AlabamaWalks.API.Profiles
{
    public class WalkDifficultyProfile : Profile
    {
        public WalkDifficultyProfile()
        {
            CreateMap<WalkDifficulty, WalkDifficultyDTO>()
                .ReverseMap(); 
            CreateMap<WalkDifficulty, AddWalkDifficultyRequest>()
                .ReverseMap();
            CreateMap<WalkDifficulty, UpdateWalkDifficultyRequest>()
                .ReverseMap();
        }
    }
}
