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
            return await _context.Walks.ToListAsync();  
        }
    }
}
