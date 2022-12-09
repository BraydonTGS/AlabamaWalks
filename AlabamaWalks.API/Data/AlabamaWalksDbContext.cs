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
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Role> Users_Roles { get; set; }


        // Overriding the OnModelCreating to define our Many to Many Relationships //
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.Role)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.RoleId);
            modelBuilder.Entity<User_Role>()
                .HasOne(x => x.User)
                .WithMany(y => y.UserRoles)
                .HasForeignKey(x => x.UserId); 
        }


    }
}
