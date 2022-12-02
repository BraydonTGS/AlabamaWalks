namespace AlabamaWalks.API.Models.DTO
{
    // Defines the Properties we need as part of the add request only //
    // This is what the user interacts with //
    public class AddRegionRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public double Population { get; set; }

    }
}
