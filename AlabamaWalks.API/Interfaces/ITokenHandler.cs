using AlabamaWalks.API.Models.Domain;

namespace AlabamaWalks.API.Interfaces
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user); 
    }
}
