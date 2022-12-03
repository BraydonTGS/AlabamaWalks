using AlabamaWalks.API.Models.Domain;
using AlabamaWalks.API.Models.DTO;
using AutoMapper;
// Profile comes from AutoMapper //
namespace AlabamaWalks.API.Profiles
{
    public class RegionsProfile : Profile
    {
        // Inside the CTOR We Create Maps for our Models //
        public RegionsProfile()
        {
            // With ForMember we can specify how we want to map //

            // Region to RegionDTO
            CreateMap<Region, RegionDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ReverseMap();
            // Region to AddRegionRequest //
            CreateMap<Region, AddRegionRequest>()
                .ReverseMap(); 
            CreateMap<Region, UpdateRegionRequest>()
                .ReverseMap();
        }

       
    }
}
