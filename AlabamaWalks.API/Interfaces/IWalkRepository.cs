using AlabamaWalks.API.Models.Domain;

namespace AlabamaWalks.API.Interfaces
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync();
        Task<Walk> GetWalkByIdAsync(Guid id); 
        Task<Walk> AddWalkAsync(Walk walk);
    }
}
