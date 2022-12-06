using AlabamaWalks.API.Models.Domain;

namespace AlabamaWalks.API.Interfaces
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>>GetAllWalkDifficultiesAsync();
        Task<WalkDifficulty> GetWalkDifficultyByIdAsync(Guid id);
        Task<WalkDifficulty> AddWalkDifficultyAsync(WalkDifficulty walkDifficulty); 
        Task<WalkDifficulty> UpdateWalkDifficultyAsync(Guid id, WalkDifficulty walkDifficulty);
        Task <WalkDifficulty> DeleteWalkDifficultyAsync(Guid id);


    }
}
