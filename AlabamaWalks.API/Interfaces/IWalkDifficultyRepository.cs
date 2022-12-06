using AlabamaWalks.API.Models.Domain;

namespace AlabamaWalks.API.Interfaces
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>>GetAllWalkDifficultiesAsync();
        Task<WalkDifficulty> GetWalkDifficultyById(Guid id);
        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty); 
    }
}
