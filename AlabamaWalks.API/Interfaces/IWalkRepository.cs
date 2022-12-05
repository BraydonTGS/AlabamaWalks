using AlabamaWalks.API.Models.Domain;

namespace AlabamaWalks.API.Interfaces
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync(); 
    }
}
