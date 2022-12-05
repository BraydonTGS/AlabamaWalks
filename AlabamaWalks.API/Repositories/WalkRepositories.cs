using AlabamaWalks.API.Data;
using AlabamaWalks.API.Interfaces;

namespace AlabamaWalks.API.Repositories
{
    public class WalkRepositories : IWalkRepository
    {
        private readonly AlabamaWalksDbContext _context;

        public WalkRepositories(AlabamaWalksDbContext context)
        {
            _context = context;
        }
    }
}
