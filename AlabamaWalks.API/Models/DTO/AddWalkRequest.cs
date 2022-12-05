namespace AlabamaWalks.API.Models.DTO
{
    public class AddWalkRequest
    { 
        // Dont need the ID because we are creating a new guid ourselves //
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
