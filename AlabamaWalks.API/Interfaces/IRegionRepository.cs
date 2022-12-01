using AlabamaWalks.API.Models.Domain;

namespace AlabamaWalks.API.Interfaces
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegionsAsync(); 
    }
}
