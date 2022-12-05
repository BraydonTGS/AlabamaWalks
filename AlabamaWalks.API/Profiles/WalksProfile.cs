using AlabamaWalks.API.Models.Domain;
using AlabamaWalks.API.Models.DTO;
using AutoMapper;

namespace AlabamaWalks.API.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile()
        {
            CreateMap<Walk, WalkDTO>()
                .ReverseMap(); 
            CreateMap<WalkDifficulty, WalkDifficultyDTO>()
                .ReverseMap();
            CreateMap<Walk, AddWalkRequest>()
                .ReverseMap(); 
            CreateMap<Walk, UpdateWalkRequest>()
                .ReverseMap();
        }
    }
}
