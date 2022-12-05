namespace AlabamaWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        // Navigation Properties //
        public Region Region { get; set; }
        public WalkDifficulty WalkDifficulty { get; set; }

    }
}

// Navigation Properties Entity Framework //
// Provides a way to nagivate an association between two entity types //
// Every object can have a navigation property for every relationship in which it participates //