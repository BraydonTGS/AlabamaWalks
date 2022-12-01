using AlabamaWalks.API.Models.Domain;

namespace AlabamaWalks.API.Interfaces
{
    public interface IRegionRepository
    {
        IEnumerable<Region> GetAllRegions(); 
    }
}
