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
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ReverseMap(); 
        }

       
    }
}
