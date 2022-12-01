using AlabamaWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AlabamaWalks.API.Data
{
    public class AlabamaWalksDbContext : DbContext
    {
        public AlabamaWalksDbContext(DbContextOptions<AlabamaWalksDbContext> options) :  base (options) { }

        // Entity uses the DbSets to Create the Tables //
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }


    }
}
