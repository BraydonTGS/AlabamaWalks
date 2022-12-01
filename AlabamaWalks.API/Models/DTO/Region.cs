namespace AlabamaWalks.API.Models.DTO
{
    // The Client Should Never Get Breaking Changes In The Current Version Of the API //
    // Insert the Use of Contract Models or DTO's - Data Transfer Objects //
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public double Population { get; set; }
    }
}
