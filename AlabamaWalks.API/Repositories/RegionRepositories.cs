using AlabamaWalks.API.Data;
using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;

namespace AlabamaWalks.API.Repositories
{
    public class RegionRepositories : IRegionRepository
    {
        private readonly AlabamaWalksDbContext _context;

        // Injecting the DbContext through the Constructor //
        public RegionRepositories(AlabamaWalksDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Region> GetAllRegions()
        {
           return _context.Regions.ToList();
        }
    }
}
