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

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficultiesAsync()
        {
            return await _context.WalkDifficulty.ToListAsync(); 
        }
    }
}
