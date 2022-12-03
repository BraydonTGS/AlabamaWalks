using AlabamaWalks.API.Data;
using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Region> AddRegionAsync(Region region)
        {
            // Override the Id property to be a new Guid //
            region.Id= Guid.NewGuid();
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;

        }

        public async Task<Region> DeleteRegionAsync(Guid id)
        {
            var region =  await _context.Regions.FirstOrDefaultAsync(x => x.Id==id);
            if (region == null)
            {
                return null;
            }
             _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
           return await _context.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionByIdAsync(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var existingRegion = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null; 
            }

            existingRegion.Code = region.Code; 
            existingRegion.Name= region.Name;
            existingRegion.Area= region.Area;
            existingRegion.Lat= region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population; 

            await _context.SaveChangesAsync();

            return existingRegion;




        }
    }
}
