using AlabamaWalks.API.Data;
using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlabamaWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly AlabamaWalksDbContext _context;

        public WalkDifficultyRepository(AlabamaWalksDbContext context)
        {
            _context = context;
        }

        public async Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id= Guid.NewGuid();
            await _context.WalkDifficulty.AddAsync(walkDifficulty);
            await _context.SaveChangesAsync();
            return walkDifficulty; 
     
        }

        public async Task<WalkDifficulty> DeleteWalkDifficultyAsync(Guid id)
        {
            var existingWalkDiff = await _context.WalkDifficulty.FindAsync(id); 
            if (existingWalkDiff != null)
            {
                _context.WalkDifficulty.Remove(existingWalkDiff);
                await _context.SaveChangesAsync();
                return existingWalkDiff; 
            }
            return null; 
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync()
        {
            return await _context.WalkDifficulty.ToListAsync(); 
        }

        public async Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid id)
        {
            return await _context.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDiff = await _context.WalkDifficulty.FindAsync(id); 
            if (existingWalkDiff != null)
            {
                existingWalkDiff.Code = walkDifficulty.Code;
                await _context.SaveChangesAsync();
                return existingWalkDiff;
            }
            return null;
          
        }


    }
}
