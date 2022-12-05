using AlabamaWalks.API.Data;
using AlabamaWalks.API.Interfaces;
using AlabamaWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlabamaWalks.API.Repositories
{
    public class WalkRepositories : IWalkRepository
    {
        private readonly AlabamaWalksDbContext _context;

        public WalkRepositories(AlabamaWalksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            // Include: Specifies related Entities to include in the Query //
            return await _context.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();  
        }
    }
}
